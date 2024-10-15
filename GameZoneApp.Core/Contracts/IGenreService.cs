using GameZoneApp.Core.Models.Game;

namespace GameZoneApp.Core.Contracts
{
    public interface IGenreService
    {
        Task<IEnumerable<GameGenreOptionModel>> GetAllAsync();
        Task<bool> ExistsByIdAsync(int id);
    }
}
