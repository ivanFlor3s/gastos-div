using ApiGastos.Entities;
using ApiGastos.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<SpentParticipant>().HasKey(sp => new { sp.SpentId, sp.UserId});
            
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
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
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


        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Spent> Spent{ get; set; }
    }
}
