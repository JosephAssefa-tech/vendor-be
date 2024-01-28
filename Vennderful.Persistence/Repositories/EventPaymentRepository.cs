using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventPaymentRepository : BaseRepository<EventPayment>, IEventPaymentRepository
    {
        private readonly VennderfulDbContext _dbContext;
        public EventPaymentRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<EventPayment> GetEventPaymentByClientId(Guid clientId, Guid eventId)
        {
            var result = (await GetQueryAsync(x => x.EventId == eventId && x.ClientId == clientId)).FirstOrDefault();
            return result;
        }
        public async Task<List<EventPayment>> GetEventPaymentsByEventId(Guid eventId)
        {
            var eventPayments = await _dbContext.EventPayments
                .Include(e => e.Client)
                .Where(e => e.EventId == eventId).ToListAsync();
            return eventPayments;
        }

    }
}
