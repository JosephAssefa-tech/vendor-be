using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Vennderful.Application.Contracts.Persitence;

namespace Vennderful.Persistence.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(VennderfulDbContext dbContext) : base(dbContext) { }

        public async Task<UserProfile> GetUserProfileByEmail(string email)
        {
            var profile = (await GetQueryAsync(x => x.Email != null && x.Email.ToLower() == email.ToLower())).FirstOrDefault();
            return profile;
        }
        public async Task<UserProfile> GetUserProfileByUserId(Guid userId)
        {
            var profile = (await GetQueryAsync(x => x.UserId != Guid.Empty && x.UserId == userId)).FirstOrDefault();
            return profile;
        }
        public async Task<IEnumerable<UserProfile>> GetInvitedUsers(string companyId)
        {
            var inviteeList = (await GetQueryAsync(x => x.CompanyId == Guid.Parse(companyId) && x.Status == "Invited")).OrderByDescending(x => x.Created).ToList();
            return inviteeList;
        }
    }      
}
