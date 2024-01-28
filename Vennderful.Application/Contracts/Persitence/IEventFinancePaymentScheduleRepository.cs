using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventFinancePaymentScheduleRepository : IAsyncRepository<EventFinancePaymentSchedule>
    {
        Task<List<EventFinancePaymentSchedule>> GetEventFinancePaymentScheduleByEventFinanceId(Guid eventFinanceId);
        Task<List<EventFinancePaymentSchedule>> GetEventFinancePaymentScheduleByEventFinanceIdAndStatus(Guid eventFinanceId);
        Task<EventFinancePaymentSchedule> GetEventFinancePaymentScheduleById(int id);
    }
}
