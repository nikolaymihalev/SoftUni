using EventMe.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMe.Core.Contracts
{
    /// <summary>
    /// Услуга на събития
    /// </summary>
    public interface IEventService
    {
        Task CreateAsync(EventModel model);
        Task DeleteAsync(int id);
        Task EditAsync(EventModel model);
        Task<EventModel> GetIdAsync(int id);
        Task<IEnumerable<EventModel>> GetAllAsync();
    }
}
