using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventFinancePaymentScheduleRepository : BaseRepository<EventFinancePaymentSchedule>, IEventFinancePaymentScheduleRepository
    {
        public EventFinancePaymentScheduleRepository(VennderfulDbContext context) : base(context)
        {

        }

        public async Task<List<EventFinancePaymentSchedule>> GetEventFinancePaymentScheduleByEventFinanceId(Guid eventFinanceId)
        {
            return (await GetQueryAsync(e => e.EventFinanceId == eventFinanceId)).ToList();
        }
        public async Task<List<EventFinancePaymentSchedule>> GetEventFinancePaymentScheduleByEventFinanceIdAndStatus(Guid eventFinanceId)
        {
            return (await GetQueryAsync(e => e.EventFinanceId == eventFinanceId && e.Status == Domain.Enums.PaymentStatus.Pending)).ToList();
        }
        public async Task<EventFinancePaymentSchedule> GetEventFinancePaymentScheduleById(int id)
        {
            return (await GetQueryAsync(e => e.Id == id)).FirstOrDefault();
        }
    }
}
