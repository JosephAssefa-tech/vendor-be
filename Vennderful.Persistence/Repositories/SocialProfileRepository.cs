using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class SocialProfileRepository : BaseRepository<SocialProfile>, ISocialProfileRepository
    {
        public SocialProfileRepository(VennderfulDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<IEnumerable<SocialProfile>> GetSocialProfilesByCompany(Guid companyId)
        {
           return await (await GetQueryAsync(x => x.CompanyId == companyId)).ToListAsync();
        
        }
    }
}
