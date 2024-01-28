using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.VenueAccount.Responses;

namespace Vennderful.Application.Features.VenueAccount.Requests
{
    public class GetVenueAccountInformationByIdQuery : IRequest<GetVenueAccountInformationResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
