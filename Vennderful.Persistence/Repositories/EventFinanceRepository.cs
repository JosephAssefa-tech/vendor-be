using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventFinanceRepository : BaseRepository<EventFinance>, IEventFinanceRepository
    {
        private readonly VennderfulDbContext _dbContext;
        public EventFinanceRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<EventFinance> GetEventFinanceByEventId(Guid eventId)
        {
            return (await GetQueryAsync(e => e.EventId == eventId)).FirstOrDefault();
        }

        public async Task<EventFinance> GetEventFinanceBudgetSummaryItemByEventId(Guid eventId, Guid packageId)
        {
            var eventFinanceSummaryItem = await _dbContext.EventFinances
                .Include(e => e.Package)
                .FirstOrDefaultAsync(e => e.EventId == eventId && e.PackageId == packageId);

            return eventFinanceSummaryItem;
        }

    
    }
}
