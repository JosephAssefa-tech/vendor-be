using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventAndClientRepository: IAsyncRepository<EventAndClient>
    {
        Task<IReadOnlyList<EventAndClient>> GetAllEventsAsync(Guid companyId);
    }
}
