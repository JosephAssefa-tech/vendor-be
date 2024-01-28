using MediatR;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Application.Features.VenueProfile.Responses;

namespace Vennderful.Application.Features.VenueProfile.Requests
{
    public class CurateVenuePublicProfileCommand : IRequest<CurateVenuePublicProfileResponse>
    {
        public CurateVenuePublicProfileDto CurateVenuePublicProfileDto { get;set;}

    }
}
