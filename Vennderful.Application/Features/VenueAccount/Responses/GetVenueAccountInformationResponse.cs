using System;

using Vennderful.Application.Common;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.VenueAccount.Responses
{
    public class GetVenueAccountInformationResponse : BaseResponse
    {
        public VenueAccountInformation Data { get; set; }
    }
}

