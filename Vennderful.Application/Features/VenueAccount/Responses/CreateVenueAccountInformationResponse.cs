using Vennderful.Application.Common;
using Vennderful.Application.Features.VenueAccount.DTOs;

namespace Vennderful.Application.Features.VenueAccount.Responses
{
    public class CreateVenueAccountInformationResponse : BaseResponse
    {
        public CreateVenueAccountInformationDto Data { get; set; }
    }
}
