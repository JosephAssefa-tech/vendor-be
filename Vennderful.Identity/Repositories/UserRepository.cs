using System;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Identity.Interfaces;
using Vennderful.Identity.Model;

namespace Vennderful.Identity.Repositories
{

    public class UserRepository : BaseIdentityRepository<ApplicationUser>, IUserRepository
    {
       


        public UserRepository(VennderfulIdentityDBContext context) : base(context)
        {
           

        }       

        public async Task<IEnumerable<ApplicationUser>> GetUserProfileByEmail(string email)
        {
            var profileList = (await GetQueryAsync(x => x.Email == email)).ToList();
            return profileList;
        }
    }
}
