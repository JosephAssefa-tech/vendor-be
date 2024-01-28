using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IAddOnRepository : IAsyncRepository<AddOn>
    {
        Task<IReadOnlyList<AddOn>> GetAllAddOnsWithCategoriesAsync(Guid companyId);
        Task<AddOn> GetAddOnsByName(string addonName);
    }
}
