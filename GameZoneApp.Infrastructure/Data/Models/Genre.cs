using System.ComponentModel.DataAnnotations;

using static GameZoneApp.Infrastructure.Data.DataConstants.Ganre;

namespace GameZoneApp.Infrastructure.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; init; } = string.Empty;

        public IEnumerable<Game> Games { get; init; } = new HashSet<Game>();
    }
}