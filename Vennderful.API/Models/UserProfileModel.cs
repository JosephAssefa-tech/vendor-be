using Vennderful.Domain.Entities;
using Vennderful.Domain.Common;
namespace Vennderful.API.Models
{
    public class UserProfileModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool InitialCreated { get; set; }
        public string Status { get; set; }
        public Guid CompanyId { get; set; }
        public UserProfileModel()
        {

        }

        public override T MapToEntity<T>()
        {
            UserProfile profile = new UserProfile();
            profile.Id = this.Id;
            profile.UserId = this.UserId;
            profile.Email = this.Email;
            profile.InitialCreated = this.InitialCreated;
            profile.Status = this.Status;
            profile.CompanyId = this.CompanyId;
            return profile as T;
        }
    }
}
