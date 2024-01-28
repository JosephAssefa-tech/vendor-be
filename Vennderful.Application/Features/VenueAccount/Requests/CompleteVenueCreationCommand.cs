using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Application.Features.VenueAccount.Responses;

namespace Vennderful.Application.Features.VenueAccount.Requests
{
    public class CompleteVenueCreationCommand : IRequest<CompleteVenueCreationResponse>
    {
        public CompleteVenueCreationDTO CompleteVenueCreationDTO { get; set; }
    }
}
