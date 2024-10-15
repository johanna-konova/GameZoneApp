using System.ComponentModel.DataAnnotations;
using static GameZoneApp.Infrastructure.Data.DataConstants.Game;
using static GameZoneApp.Core.Constants.ModelsMessagesConstants;

namespace GameZoneApp.Core.Models.Game
{
    public class GameFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(
            TitleMaxLength,
            MinimumLength = TitleMinLength,
            ErrorMessage = InvalidStringLength)]
        public string Title { get; init; } = string.Empty;

        [Url(ErrorMessage = InvalidUrlFormat)]
        public string? ImageUrl { get; init; } = null;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(DescriptionMaxLength,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = InvalidStringLength)]
        public string Description { get; init; } = string.Empty;

        [Required]
        public string ReleasedOn { get; init; } = string.Empty;

        public int GenreId { get; init; }

        public IEnumerable<GameGenreOptionModel> Genres { get; set; }
            = new HashSet<GameGenreOptionModel>();
    }
}
