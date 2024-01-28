using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class VenueAccountInformationRepository : BaseRepository<VenueAccountInformation>, IVenueAccountInformationRepository
    {
        public VenueAccountInformationRepository(VennderfulDbContext dbContext) : base(dbContext) { }
        public async Task<VenueAccountInformation> GetVenueByCompanyName(string companyName, Guid companyId)
        {
            var venue = (await GetQueryAsync(x => x.CompanyName != null && x.CompanyName.ToLower() == companyName.ToLower() || x.CompanyId == companyId)).FirstOrDefault();
            return venue;
        }
        public async Task<VenueAccountInformation> GetById(Guid companyId)
        {
            var venue = (await GetQueryAsync(x =>x.CompanyId == companyId)).FirstOrDefault();
            return venue;
        }
    }
}
