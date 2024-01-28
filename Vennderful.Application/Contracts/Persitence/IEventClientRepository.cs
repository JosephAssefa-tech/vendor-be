using System.Threading.Tasks;
using System;
using Vennderful.Domain.Entities;
using System.Collections.Generic;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventClientRepository : IAsyncRepository<EventClient>
    {
        Task<EventClient> GetByEventAndClientId(Guid clientId, Guid eventId);
        Task<List<EventClient>> GetEventClientsByEventId(Guid id);


    }
}
