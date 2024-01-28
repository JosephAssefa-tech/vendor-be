using MediatR;
using System;
using Vennderful.Application.Features.Customers.Responses;

namespace Vennderful.Application.Features.Customers.Requests
{
    public class GetCustomerRequest : IRequest<GetCustomerResponse>
    {
        public Guid Id { get; set; }
    }
}
