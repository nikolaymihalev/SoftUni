using EventMe.Infrastructure.Data.Configuration;
using EventMe.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventMe.Infrastructure.Data
{
    /// <summary>
    /// Контекст на базата данни
    /// </summary>
    public class EventMeDbContext:DbContext
    {
        /// <summary>
        /// Контруктор на контекста на базата
        /// </summary>
        /// <param name="options">Настройки</param>
        public EventMeDbContext(DbContextOptions<EventMeDbContext> options):base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventConfiguration).Assembly);
        }

        /// <summary>
        /// Евенти
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// Градове
        /// </summary>
        public DbSet<Town> Towns { get; set; }

        /// <summary>
        /// Адреси
        /// </summary>
        public DbSet<Address> Addresses { get; set; }
    }
}
