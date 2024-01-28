using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface ISocialProfileRepository : IAsyncRepository<SocialProfile>
    {
        Task<IEnumerable<SocialProfile>> GetSocialProfilesByCompany(Guid companyId);
    }
}

