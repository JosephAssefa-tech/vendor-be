using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventTimelineRepository : BaseRepository<EventTimeline>, IEventTimeineRepository
    {
        private readonly VennderfulDbContext _dbContext;

        public EventTimelineRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<EventTimeline>> GetEventTimelineByEventId(Guid eventId)
        {
            return (await GetQueryAsync(e => e.EventId == eventId)).ToList();
        }
    }
}
