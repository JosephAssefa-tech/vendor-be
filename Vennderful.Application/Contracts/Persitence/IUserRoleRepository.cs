using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IUserRoleRepository : IAsyncRepository<UserRole>
    {
        Task<UserRole> GetByCompanyIdAsync(Guid companyId);
     
    }
}
