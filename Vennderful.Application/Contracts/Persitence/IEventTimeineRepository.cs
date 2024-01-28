using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventTimeineRepository : IAsyncRepository<EventTimeline>
    {
        Task<List<EventTimeline>> GetEventTimelineByEventId(Guid eventId);
    }
}
