using System;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventFinanceRepository : IAsyncRepository<EventFinance>
    {
        Task<EventFinance> GetEventFinanceByEventId(Guid eventId);
        Task<EventFinance> GetEventFinanceBudgetSummaryItemByEventId(Guid eventeId, Guid packageId);
       
    }
}
