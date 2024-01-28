using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
        Task<IReadOnlyList<Event>> GetAllEventsAsync(Guid companyId);
        Task<List<Event>> GetAllWithClientsVenueRoomsAsync(Guid companyId);
        Task<Event> GetEventsByName(string eventsName);

        Task<Event> GetById(Guid Id);

        Task<Event> GetById(Guid eventId, Guid comapnyId);

    }
}
