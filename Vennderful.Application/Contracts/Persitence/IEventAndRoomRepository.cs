using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventAndRoomRepository : IAsyncRepository<EventAndRoom>
    {
        Task<IReadOnlyList<EventAndRoom>> GetEventAndRooms(Guid companyId);
        Task<bool> CheckEventAndRoomExists(Guid companyId, string eventName);
        Task<IReadOnlyList<EventAndRoom>> GetEventAndRoomsByEventId(Guid eventId);
    }
}
