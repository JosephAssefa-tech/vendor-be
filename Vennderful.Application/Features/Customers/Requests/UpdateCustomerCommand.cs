using MediatR;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.Customers.Responses;

namespace Vennderful.Application.Features.Customers.Requests
{
    public class UpdateCustomerCommand : IRequest<UpdateCustomerResponse>
    {
        public UpdateCustomerDTO UpdateCustomerDTO { get; set; }
    }
}
