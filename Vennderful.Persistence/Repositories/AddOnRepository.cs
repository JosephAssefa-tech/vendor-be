using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class AddOnRepository : BaseRepository<AddOn>, IAddOnRepository
    {
        private readonly VennderfulDbContext _dbContext;

        public AddOnRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<IReadOnlyList<AddOn>> GetAllAddOnsWithCategoriesAsync(Guid companyId)
        {
            return await (await GetQueryAsync(x =>x.CompanyId == companyId)).Include(a => a.AddOnCategory).Include(a => a.RateStructure).ToListAsync();
        }

        public async Task<AddOn> GetAddOnsByName(string  addonName)
        {
            return await (await GetQueryAsync(x => x.AddOnName.ToLower() == addonName.ToLower())).Include(a => a.AddOnCategory).Include(a => a.RateStructure).FirstOrDefaultAsync();
        }
    }
}
