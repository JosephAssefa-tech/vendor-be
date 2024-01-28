using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventAndMemberRepository : IAsyncRepository<EventAndMember>
    {
        Task<List<EventAndMember>> GetMembersByEventId(Guid eventId);
    }
}
