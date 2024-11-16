using Divtos.Domain.Entities;
using Divtos.Infraestructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Divtos.Infraestructure.Common.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions options) : base(options)
        {
            _httpContextAccessor = this.GetService<IHttpContextAccessor>();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //Creo llavo primaria compuesta
            modelBuilder.Entity<GroupUser>().HasKey(gu => new { gu.GroupId, gu.UserId });

            modelBuilder.Entity<User>().Property(e => e.Id).HasConversion<string>();

            modelBuilder.Entity<Spent>()
             .Property(e => e.SpentMode)
             .HasConversion<int>()
             .HasDefaultValue(SpentMode.EQUALLY);

            modelBuilder.Entity<SpentParticipant>().HasKey(sp => new { sp.SpentId, sp.UserId });
            
            modelBuilder.Entity<SpentParticipant>()
              .HasOne(sp => sp.Spent)
              .WithMany(s => s.Participants)
              .HasForeignKey(sp => sp.SpentId);

            modelBuilder.Entity<SpentParticipant>()
                .HasOne(sp => sp.User)
                .WithMany(au => au.SpentParticipants)
                .HasForeignKey(sp => sp.UserId);

            //QUERY FILTERS
            modelBuilder.Entity<Group>()
               .HasQueryFilter(g => g.GroupUsers.Any(gu => gu.UserId == Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetId())));



        }
        // OVERRIDE SAVES
        public override int SaveChanges()
        {
            UpdateAuditFields();
            UpdateSoftDeleteFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            UpdateSoftDeleteFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Auditable && (e.State == EntityState.Added || e.State == EntityState.Modified));

            var currentUserId = _httpContextAccessor.HttpContext.User.Identity.GetId();

            foreach (var entry in entries)
            {
                var auditable = (Auditable)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedAt = DateTime.UtcNow;
                    auditable.CreatorId = Guid.Parse(currentUserId);
                }

                auditable.LastModified = DateTime.UtcNow;
                auditable.LastModifiedById = Guid.Parse(currentUserId);
            }
        }

        private void UpdateSoftDeleteFields()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is SoftDeleteEntity && e.State == EntityState.Deleted);
            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                var softDeleteEntity = (SoftDeleteEntity)entry.Entity;
                softDeleteEntity.IsDeleted = true;
                softDeleteEntity.DeletedAt = DateTime.UtcNow;
                softDeleteEntity.DeletedById = _httpContextAccessor.HttpContext.User.Identity.GetId();
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Spent> Spents { get; set; }
        public DbSet<SpentParticipant> SpentUsers { get; set; }



    }
}
