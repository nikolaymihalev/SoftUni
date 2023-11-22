using EventMe.Infrastructure.Data.Contracts;

namespace EventMe.Infrastructure.Data.Common
{
    /// <summary>
    /// Методи за достъп на данни
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Добавяне на елемент в базата данни
        /// </summary>
        /// <typeparam name="T">Тип на елемент</typeparam>
        /// <param name="entity">Елемент</param>
        /// <returns></returns>
        Task AddAsync<T>(T entity) where T : class;

        /// <summary>
        /// Изтриване на елемент от базата данни
        /// </summary>
        /// <typeparam name="T">Тип на елемент</typeparam>
        /// <param name="entity">Елемент</param>
        void Delete<T>(T entity) where T : class, IDeletable;

        /// <summary>
        /// Извличане на елементи от таблица
        /// </summary>
        /// <typeparam name="T">Тип на елементите</typeparam>
        /// <returns></returns>
        IQueryable<T> All<T>() where T : class;

        /// <summary>
        /// Извличане на елементи от таблица включително изтритите
        /// </summary>
        /// <typeparam name="T">Тип на елементите</typeparam>
        /// <returns></returns>
        IQueryable<T> AllWithDeleted<T>() where T : class, IDeletable;
        
        /// <summary>
        /// Извличане на елементи от таблица само за четене
        /// </summary>
        /// <typeparam name="T">Тип на елементите</typeparam>
        /// <returns></returns>
        IQueryable<T> AllReadOnly<T>() where T : class;

        /// <summary>
        /// Извличане на елементи от таблица включително изтритите само за четене
        /// </summary>
        /// <typeparam name="T">Тип на елементите</typeparam>
        /// <returns></returns>
        IQueryable<T> AllWithDeletedReadOnly<T>() where T : class, IDeletable;

        /// <summary>
        /// Извличане на елемент по индентификатор
        /// </summary>
        /// <typeparam name="T">Тип на елементите</typeparam>
        /// <returns></returns>
        Task<T> GetById<T>(int id) where T : class;

        /// <summary>
        /// Запис на промените в базата данни
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
