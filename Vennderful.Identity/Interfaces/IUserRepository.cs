using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Identity.Interfaces;
using Vennderful.Domain.Entities;
using Vennderful.Identity.Model;

namespace Vennderful.Identity.Interfaces
{
    public interface IUserRepository : IAsyncIdentityRepository<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetUserProfileByEmail(string email);
        
    }
}
