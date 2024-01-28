using Vennderful.Application.Common;
using Vennderful.Application.Features.VenueProfile.DTOs;

namespace Vennderful.Application.Features.VenueProfile.Responses
{
    public class CurateVenuePublicProfileResponse : BaseResponse
    {
        public CurateVenuePublicProfileDto Data { get; set; }
    }
}
