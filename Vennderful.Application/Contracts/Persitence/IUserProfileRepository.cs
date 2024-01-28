using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IUserProfileRepository: IAsyncRepository<UserProfile>
    {
        Task<UserProfile> GetUserProfileByEmail(string email);
        Task<UserProfile> GetUserProfileByUserId(Guid userId);
        Task<IEnumerable<UserProfile>> GetInvitedUsers(string companyId);
    }  
}
