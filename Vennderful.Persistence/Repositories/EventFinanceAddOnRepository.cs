using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventFinanceAddOnRepository : BaseRepository<EventFinanceAddOn>, IEventFinanceAddOnRepository
    {
        private readonly VennderfulDbContext _dbContext;
        public EventFinanceAddOnRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<EventFinanceAddOn>> GetEventFinanceAddOnByEventFinanceId(Guid eventFinanceId)
        {
            return (await GetQueryAsync(e => e.EventFinanceId == eventFinanceId)).ToList();
        }
        public async Task<EventFinanceAddOn> GetEVentFinanceAddonsByEventFinanceIdAndAddonId(Guid eventFinanceId, Guid addOnId)
        {
            var eventFinanceAddons = await _dbContext.EventFinanceAddOns
                .Include(e => e.AddOn)
                .FirstOrDefaultAsync(e => e.EventFinanceId == eventFinanceId && e.AddOnId == addOnId);

            return eventFinanceAddons;
        }
    }
}
