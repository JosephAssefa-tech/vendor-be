using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private readonly VennderfulDbContext _dbContext;

        public EventRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<Event> GetById(Guid Id)
        {
            var result = (await GetQueryAsync(x => x.Id == Id)).FirstOrDefault();
            return result;
        }
        public async Task<IReadOnlyList<Event>> GetAllEventsAsync(Guid companyId)
        {
            return await (await GetQueryAsync(x => x.CompanyId == companyId)).ToListAsync();
        }

        public async Task<Event> GetEventsByName(string eventName)
        {
            return await (await GetQueryAsync(x => x.EventName.ToLower() == eventName.ToLower())).FirstOrDefaultAsync();
        }

        public async Task<Event> GetById(Guid eventId, Guid comapnyId)
        {
            return await (await GetQueryAsync(x => x.Id == eventId && x.CompanyId == comapnyId)).FirstOrDefaultAsync();
        }

        public async Task<List<Event>> GetAllWithClientsVenueRoomsAsync(Guid companyId)
        {
            var events = await _dbContext.Events
                .Include(e => e.EventClients).ThenInclude(ec=>ec.Client)
                //.Include(e => e.EventAndRooms).ThenInclude(ec=>ec.Rooms)s
                .ToListAsync();

            return events;
        }
    }
}
