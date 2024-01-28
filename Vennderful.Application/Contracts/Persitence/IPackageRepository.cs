using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IPackageRepository : IAsyncRepository<Package>
    {
        Task<Package> GetPackageByName(string packageName);
        Task<IEnumerable<Package>> GetAllPackages(Guid companyId);
    }
}
