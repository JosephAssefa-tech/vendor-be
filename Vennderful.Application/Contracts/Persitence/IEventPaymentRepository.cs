using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventPaymentRepository : IAsyncRepository<EventPayment>
    {
        Task<EventPayment> GetEventPaymentByClientId(Guid clientId,Guid eventId);
        Task<List<EventPayment>> GetEventPaymentsByEventId(Guid eventId);

    }
}
