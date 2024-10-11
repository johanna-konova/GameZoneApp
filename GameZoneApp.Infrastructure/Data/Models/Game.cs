using Microsoft.Models.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameZoneApp.Infrastructure.Data.DataConstants.Game;

namespace GameZoneApp.Infrastructure.Data.Models
{
    public class Game
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; init; } = string.Empty;

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; init; } = string.Empty;

        public string? ImageUrl { get; init; } = null;

        public DateTime ReleasedOn { get; init; }

        public Guid PublisherId { get; init; }

        [Required]
        [ForeignKey(nameof(PublisherId))]
        public ApplicationUser Publisher { get; init; } = null!;

        public int GenreId { get; init; }

        [Required]
        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; init; } = null!;

        public IEnumerable<GamersGames> GamerGame = new HashSet<GamersGames>();
    }
}
