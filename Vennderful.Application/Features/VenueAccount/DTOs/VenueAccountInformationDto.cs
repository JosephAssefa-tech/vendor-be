using Vennderful.Application.Common;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.VenueAccount.DTOs
{
    public class VenueAccountInformationDto: BaseDTO
    {
        public string CompanyName { get; set; }
        public string? Website { get; set; }
        public string PhoneNumber { get; set; }
         public int TypeOfBusinessId { get; set; }
        public virtual Address Address { get; set; }

    }
}
