using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IMemberRepository : IAsyncRepository<Member>
    {
        Task<IEnumerable<Member>> GetMembersByCompanyId(Guid comapnyId);
    }
}
