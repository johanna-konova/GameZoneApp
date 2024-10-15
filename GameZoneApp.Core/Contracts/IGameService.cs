using GameZoneApp.Core.Models.Game;

namespace GameZoneApp.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameViewModel>> GetAllAsync();
        Task AddAsync(GameFormModel model, Guid userId);
    }
}
