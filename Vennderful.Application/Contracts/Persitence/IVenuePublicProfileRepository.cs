using System;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IVenuePublicProfileRepository : IAsyncRepository<VenuePublicProfile>
    {
        Task<VenuePublicProfile> GetProfileByCompanyId(Guid companyId);
    }
}
