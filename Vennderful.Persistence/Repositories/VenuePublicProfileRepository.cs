using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class VenuePublicProfileRepository : BaseRepository<VenuePublicProfile>, IVenuePublicProfileRepository
    {
        public VenuePublicProfileRepository(VennderfulDbContext dbContext) : base(dbContext)
        {            
        }
        public async Task<VenuePublicProfile> GetProfileByCompanyId(Guid companyId)
        {
            return (await GetQueryAsync(x => x.VenueAccountInformationId == companyId)).Include(a => a.WorkingHour).FirstOrDefault();          
        }
    }
}
