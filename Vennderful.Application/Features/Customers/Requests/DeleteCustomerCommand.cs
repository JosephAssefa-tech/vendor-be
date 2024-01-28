using MediatR;
using System;
using Vennderful.Application.Features.Customers.Responses;

namespace Vennderful.Application.Features.Customers.Requests
{
    public class DeleteCustomerCommand : IRequest<DeleteCustomerResponse>
    {
        public Guid Id { get; set; }
    }
}
