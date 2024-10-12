using GameZoneApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Models.DependencyInjection;

using static GameZoneApp.Infrastructure.Data.DataSeeder;

namespace GameZoneApp.Infrastructure.Data
{
    public class GameZoneDbContext : IdentityDbContext<ApplicationUser,  IdentityRole<Guid>, Guid>
    {
        public GameZoneDbContext(DbContextOptions<GameZoneDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<GamersGames> GamerGame { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Game>()
                .HasOne(g => g.Publisher)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Genre>()
                .HasData(GenerateGenres());

            base.OnModelCreating(builder);
        }
    }
}
