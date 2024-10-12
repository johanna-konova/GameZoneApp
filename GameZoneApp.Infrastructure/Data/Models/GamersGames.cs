using Microsoft.EntityFrameworkCore;
using Microsoft.Models.DependencyInjection;

using System.ComponentModel.DataAnnotations.Schema;

namespace GameZoneApp.Infrastructure.Data.Models
{
    [PrimaryKey(nameof(GameId), nameof(GamerId))]
    public class GamersGames
    {
        public Guid GameId { get; init; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; init; } = null!;

        public Guid GamerId { get; init; }

        [ForeignKey(nameof(GamerId))]
        public ApplicationUser Gamer { get; init; } = null!;
    }
}