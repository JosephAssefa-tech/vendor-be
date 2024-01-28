using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.VenueAccount.DTOs;

namespace Vennderful.Application.Features.VenueAccount.Responses
{
    public class CompleteVenueCreationResponse : BaseResponse
    {
        public CompleteVenueCreationDTO Data { get; set; }
}
}
