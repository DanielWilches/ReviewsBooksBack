using Books.EnterpriseBusiness.Layer.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Books.InterfaceAdapter.Layer
{
    public class AppDbConext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        public AppDbConext(DbContextOptions<AppDbConext> options) : base(options)
        {
        }
        
        public DbSet<BookEntity> books { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<UserEntity> users { get; set; }
        public DbSet<CustomUserProfile> CustomUserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.ModifiedDate);
                entity.Property(e => e.RefreshToken);
                entity.Property(e => e.RefreshTokenExpiryTime);

            });
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Author).HasMaxLength(100);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.CreatedDate);
            });

            modelBuilder.Entity<ReviewEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Rating).IsRequired();
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.BookId).IsRequired();
                      
            });

            modelBuilder.Entity<CustomUserProfile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IdentityUserId).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.HasOne<UserEntity>()
                      .WithMany()
                      .HasForeignKey(e => e.IdentityUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
