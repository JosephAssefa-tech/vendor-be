using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Application.Features.VenueProfile.Responses;

namespace Vennderful.Application.Features.VenueProfile.Requests
{
    public class UpdateVenuePublicProfileCommand : IRequest<UpdateVenuePublicProfileResponse>
    {
        public UpdateVenuePublicProfileDTO UpdateVenuePublicProfileDTO { get; set; }

    }
}
