using MediatR;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Models.Mail;

namespace Vennderful.Application.Features.Customers.Requests
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResponse>
    {
        public Email email { get; set; }
    }
}
