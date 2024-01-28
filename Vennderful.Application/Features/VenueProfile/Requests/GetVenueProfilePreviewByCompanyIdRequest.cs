using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.VenueAccount.Responses;
using Vennderful.Application.Features.VenueProfile.Responses;

namespace Vennderful.Application.Features.VenueProfile.Requests
{
    public  class GetVenueProfilePreviewByCompanyIdRequest : IRequest<GetVenueProfilePreviewByCompanyIdResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
