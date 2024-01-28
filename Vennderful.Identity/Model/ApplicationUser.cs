using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Vennderful.Identity;

namespace Vennderful.Identity.Model
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this), authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }

}
