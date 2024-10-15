using GameZoneApp.Core.Models.Game;

namespace GameZoneApp.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameViewModel>> GetAllAsync();
        Task<IEnumerable<GameViewModel>> GetByUserIdAsync(Guid userId);
        Task<GameDetailsViewModel?> GetDetailsAsync(Guid id);
        Task<GameFormModel?> AssembleGameFormModelAsync(Guid id);
        Task<GameDeleteViewModel?> GetDetailsForDeleteFormAsync(Guid id);
        Task<bool> ExistsById(Guid id);
        Task<bool> IsUserGameCreatorAsync(Guid gameId, Guid userId);
        Task AddAsync(GameFormModel model, Guid userId);
        Task EditAsync(Guid id, GameFormModel model);
        Task DeleteAsync(Guid id);
        Task AddToZoneAsync(Guid gameId, Guid userId);
        Task StrikeOutAsync(Guid gameId, Guid userId);
    }
}
