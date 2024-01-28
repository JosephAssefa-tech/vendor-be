using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.RateStructure.Responses;
using Vennderful.Application.Features.User.Responses;

namespace Vennderful.Application.Features.RateStructure.Requests
{
    public class GetRateStructuresRequest : IRequest<GetRateStructuresResponse>
    {
    }
}
