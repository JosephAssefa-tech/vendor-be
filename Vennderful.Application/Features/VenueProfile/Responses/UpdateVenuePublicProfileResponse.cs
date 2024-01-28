using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.VenueProfile.DTOs;

namespace Vennderful.Application.Features.VenueProfile.Responses
{
    public class UpdateVenuePublicProfileResponse : BaseResponse
    {
        public UpdateVenuePublicProfileDTO Data { get; set; }
    }
}
