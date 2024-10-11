using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Models.DependencyInjection;

namespace GameZoneApp.Infrastructure.Data
{
    public class GameZoneDbContext : IdentityDbContext<ApplicationUser,  IdentityRole<Guid>, Guid>
    {
        public GameZoneDbContext(DbContextOptions<GameZoneDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);

            //builder
            //    .Entity<Genre>()
            //    .HasData(
            //    new Genre { Id = 1, Name = "Action" },
            //    new Genre { Id = 2, Name = "Adventure" },
            //    new Genre { Id = 3, Name = "Fighting" },
            //    new Genre { Id = 4, Name = "Sports" },
            //    new Genre { Id = 5, Name = "Racing" },
            //    new Genre { Id = 6, Name = "Strategy" });
        }
    }
}
