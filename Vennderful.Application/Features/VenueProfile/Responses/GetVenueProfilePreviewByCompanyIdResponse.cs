using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.VenueProfile.DTOs;

namespace Vennderful.Application.Features.VenueProfile.Responses
{
    public  class GetVenueProfilePreviewByCompanyIdResponse : BaseResponse
    {
       public  GetVenueProfilePreviewByCompanyIdResponseDTO Data { get; set; }
    }
}
