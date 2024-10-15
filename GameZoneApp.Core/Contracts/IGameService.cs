using GameZoneApp.Core.Models.Game;

namespace GameZoneApp.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameViewModel>> GetAllAsync();
        Task<GameFormModel?> AssembleGameFormModelAsync(Guid id);
        Task<bool> ExistsById(Guid id);
        Task<bool> IsUserGameCreatorAsync(Guid gameId, Guid userId);
        Task AddAsync(GameFormModel model, Guid userId);
        Task EditAsync(Guid id, GameFormModel model);
    }
}
