using ApiGastos.Core.Share.Enums;
using ApiGastos.Entities;
using ApiGastos.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiGastos
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Creo llavo primaria compuesta
            modelBuilder.Entity<GroupUser>().HasKey(gu => new { gu.GroupId, gu.AppUserId });

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

            modelBuilder.Entity<Group>()
                .HasQueryFilter(g => g.GroupUsers.Any(gu => gu.AppUserId == _httpContextAccessor.HttpContext.User.Identity.GetId()));

            modelBuilder.Entity<Invitation>()
               .HasOne(i => i.Group)
               .WithMany(g => g.Invitations)
               .HasForeignKey(i => i.GroupId);

            modelBuilder.Entity<Invitation>()
                .Property(i => i.InvitationStatus)
                .HasDefaultValue(InvitationStatus.PENDING);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(SoftDeleteEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = SetSoftDeleteFilterMethod.MakeGenericMethod(entityType.ClrType);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }


        }

        private static readonly MethodInfo SetSoftDeleteFilterMethod =
        typeof(ApplicationDbContext).GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static);

        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : SoftDeleteEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }


        public override int SaveChanges()
        {
            UpdateAuditFields();
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

            foreach (var entry in entries)
            {
                var auditable = (Auditable)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedAt = DateTime.UtcNow;
                    auditable.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.GetId();
                }

                auditable.LastModified = DateTime.UtcNow;
                auditable.LastModifiedBy = _httpContextAccessor.HttpContext.User.Identity.GetId();
            }
        }

        private void UpdateSoftDeleteFields()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is SoftDeleteEntity && e.State == EntityState.Deleted);
            foreach (var entry in entries)
            {
                var softDeleteEntity = (SoftDeleteEntity)entry.Entity;
                softDeleteEntity.IsDeleted = true;
                softDeleteEntity.DeletedAt = DateTime.UtcNow;
                softDeleteEntity.DeletedById = _httpContextAccessor.HttpContext.User.Identity.GetId();
            }
        }


        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Spent> Spent{ get; set; }
        public DbSet<Invitation> Invitations { get; set;}
    }
}
