using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class PackageRepository : BaseRepository<Package>, IPackageRepository
    {
        public PackageRepository(VennderfulDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Package>> GetAllPackages(Guid companyId)
        {
            return await (await GetQueryAsync(x => x.CompanyId == companyId)).Include(a => a.PackageCategory).Include(a => a.RateStructure).ToListAsync();
        }

        public async Task<Package> GetPackageByName(string packageName)
        {
            return await (await GetQueryAsync(x => x.PackageName.ToLower() == packageName.ToLower())).Include(a => a.PackageCategory).Include(a => a.RateStructure).FirstOrDefaultAsync();
        }
    }
}
