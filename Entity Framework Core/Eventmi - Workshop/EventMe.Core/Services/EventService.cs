using EventMe.Core.Contracts;
using EventMe.Core.Models;
using EventMe.Infrastructure.Data.Common;
using EventMe.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMe.Core.Services
{
    public class EventService : IEventService
    {
        readonly IRepository eventRepository;

        public EventService(IRepository _eventRepository)
        {
            eventRepository = _eventRepository;
        }

        public async Task CreateAsync(EventModel model)
        {
            if (model.Id > 0) 
            {
                bool exist = eventRepository.GetById<Event>(model.Id) != null;
                throw new ArgumentException("Събитието вече съществува");
            }

            Event newEvent = new Event
            {
                Name = model.Name,
                Start = model.Start,
                End = model.End,
                IsActive = true,
                Place = new Address()
                {
                    StreetAddress = model.StreetAddress,
                    TownId = model.TownId
                }
            };

            await eventRepository.AddAsync(newEvent);
            await eventRepository.SaveChangesAsync();  
        }

        public async Task DeleteAsync(int id)
        {
            Event? currentEvent = await eventRepository.GetById<Event>(id);

            if (currentEvent != null)
            {
                eventRepository.Delete(currentEvent);
                await eventRepository.SaveChangesAsync();
            }
        }

        public async Task EditAsync(EventModel model)
        {
            Event? currentEvent = await eventRepository.GetById<Event>(model.Id);

            if (currentEvent == null) 
            {
                throw new ArgumentException("Събитието не съществува");
            }

            currentEvent.Name = model.Name;
            currentEvent.Start = model.Start;
            currentEvent.End = model.End;
            currentEvent.Place.StreetAddress = model.StreetAddress;
            currentEvent.Place.TownId = model.TownId;

            await eventRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventModel>> GetAllAsync()
        {
            return await eventRepository.AllReadOnly<Event>().Select(e => new EventModel
            {
                Id = e.Id,
                Name = e.Name,
                Start = e.Start,
                End = e.End,
                StreetAddress = e.Place.StreetAddress,
                TownId = e.Place.TownId
            })
            .ToListAsync();
        }

        public async Task<EventModel> GetIdAsync(int id)
        {
            Event? currentEvent = await eventRepository
                .AllReadOnly<Event>()
                .FirstOrDefaultAsync(e=>e.Id == id);

            if (currentEvent == null)
            {
                throw new ArgumentException("Събитието не съществува");
            }
            return new EventModel()
            {
               Id = currentEvent.Id,
               Name = currentEvent.Name,
               Start = currentEvent.Start,
               End = currentEvent.End,
               StreetAddress = currentEvent.Place.StreetAddress,
               TownId = currentEvent.Place.TownId
            };
        }
    }
}
