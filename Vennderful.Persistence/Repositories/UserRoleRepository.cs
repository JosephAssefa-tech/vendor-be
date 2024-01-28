using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(VennderfulDbContext context) : base(context)
        {

        }
        public async Task<UserRole> GetByCompanyIdAsync(Guid CompanyId)
        {
            var profile = (await GetQueryAsync(x => x.CompanyId != Guid.Empty && x.CompanyId == CompanyId)).FirstOrDefault();
            return profile;
        }

    }
}
