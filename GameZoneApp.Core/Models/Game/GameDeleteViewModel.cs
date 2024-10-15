namespace GameZoneApp.Core.Models.Game
{
    public class GameDeleteViewModel
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Publisher { get; init; } = string.Empty;
    }
}
