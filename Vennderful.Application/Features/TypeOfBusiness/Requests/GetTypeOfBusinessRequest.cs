using MediatR;
using System;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.TypeOfBusiness.Responses;

namespace Vennderful.Application.Features.TypeOfBusiness.Requests
{
    public class GetTypeOfBusinessRequest: IRequest<GetTypeOfBusinessResponse>
    {
        public Guid Id { get; set; }
    }
}
