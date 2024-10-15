namespace GameZoneApp.Core.Models.Game
{
    public class GameViewModel
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? ImageUrl { get; init; } = string.Empty;
        public string ReleasedOn { get; init; } = string.Empty;
        public string Genre { get; init; } = string.Empty;
        public string Publisher { get; init; } = string.Empty;
    }
}
