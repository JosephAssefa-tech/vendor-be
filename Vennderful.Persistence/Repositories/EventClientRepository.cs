using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventClientRepository : BaseRepository<EventClient>, IEventClientRepository
    {
        private readonly VennderfulDbContext _dbContext;

        public EventClientRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;
        }
    

        public async Task<EventClient> GetByEventAndClientId(Guid clientId, Guid eventId)
        {
            var eventClient = (await GetQueryAsync(x => x.ClientId == clientId && x.EventId == eventId)).FirstOrDefault();
            return eventClient;
        }

        public async Task<List<EventClient>> GetEventClientsByEventId(Guid id)
        {
           // return (await GetQueryAsync(e => e.EventId == id)).ToList();

            var events = await _dbContext.EventClients
                  .Include(e => e.Client).Where(e=>e.EventId==id)
                  //.Include(e => e.EventAndRooms).ThenInclude(ec=>ec.Rooms)s
                  .ToListAsync();

            return events;

        }
    }
}
