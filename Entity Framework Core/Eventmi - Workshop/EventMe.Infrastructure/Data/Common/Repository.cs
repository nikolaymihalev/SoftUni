using EventMe.Infrastructure.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EventMe.Infrastructure.Data.Common
{
    /// <summary>
    /// Репозитори
    /// </summary>
    public class Repository : IRepository
    {
        readonly EventMeDbContext dbContext;
        /// <summary>
        /// Конструктор за инжектиране на контекста на базата
        /// </summary>
        /// <param name="_dbContext"></param>
        public Repository(EventMeDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Връща DbSet за даден тип
        /// </summary>
        /// <typeparam name="T">Тип на entity</typeparam>
        /// <returns></returns>

        DbSet<T> GetDbSet<T>() where T : class => dbContext.Set<T>();

        public async Task AddAsync<T>(T entity) where T : class
        {
            await GetDbSet<T>().AddAsync(entity);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return GetDbSet<T>();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return GetDbSet<T>().AsNoTracking();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync(); 
        }

        public IQueryable<T> AllWithDeleted<T>() where T : class, IDeletable
        {
            return GetDbSet<T>().IgnoreQueryFilters();
        }

        public IQueryable<T> AllWithDeletedReadOnly<T>() where T : class, IDeletable
        {
            return GetDbSet<T>().IgnoreQueryFilters().AsNoTracking();
        }

        public void Delete<T>(T entity) where T : class, IDeletable
        {
            entity.IsActive = false;
            entity.DeletedOn = DateTime.Now;
        }

        public async Task<T> GetById<T>(int id) where T : class
        {
            return await GetDbSet<T>().FindAsync(id);
        }
    }
}
