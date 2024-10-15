namespace GameZoneApp.Infrastructure.Common
{
    public interface IRepository
    {
        IQueryable<T> All<T>() where T : class;
        IQueryable<T> AllAsNoTracking<T>() where T : class;
        Task<T?> GetByIdAsync<T>(Guid id) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        Task<int> SaveChangesAsync();
    }
}
