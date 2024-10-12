using GameZoneApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameZoneApp.Infrastructure.Common
{
    public class Repository : IRepository
    {
        private readonly DbContext context;

        public Repository(GameZoneDbContext _context)
        {
            context = _context;
        }

        private DbSet<T> DbSet<T>() where T : class
            => context.Set<T>();

        public IQueryable<T> All<T>() where T : class
            => DbSet<T>();

        public IQueryable<T> AllAsNoTracking<T>() where T : class
            => DbSet<T>().AsNoTracking();

        public async Task<T?> GetByIdAsync<T>(Guid id) where T : class
            => await DbSet<T>().FindAsync(id);

        public async Task AddAsync<T>(T entity) where T : class
            => await DbSet<T>().AddAsync(entity);

        public async Task<int> SaveChangesAsync()
            => await context.SaveChangesAsync();
    }
}
