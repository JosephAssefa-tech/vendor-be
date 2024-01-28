using System;
using Vennderful.Application.Common;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.VenueAccount.DTOs
{
    public class CreateVenueAccountInformationDto: BaseDTO
    {
        public string CompanyName { get; set; }
        public string? Website { get; set; }
        public string PhoneNumber { get; set; }
        public Guid TypeOfBusinessId { get; set; }
        public  Address Address { get; set; }
        public Guid CompanyId { get; set; }

    }
}
