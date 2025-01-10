using Microsoft.EntityFrameworkCore;
using WebApp_MVC_auth_cookiee.Models;

namespace WebApp_MVC_auth_cookiee.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка отношений между моделями
            modelBuilder.Entity<Place>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Place)
                .HasForeignKey(c => c.PlaceId);

            modelBuilder.Entity<Place>()
                .HasMany(p => p.Ratings)
                .WithOne(r => r.Place)
                .HasForeignKey(r => r.PlaceId);
        }
    }
}
