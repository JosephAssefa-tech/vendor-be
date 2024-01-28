using MediatR;
using Vennderful.Application.Features.EventPayment.DTOs;
using Vennderful.Application.Features.EventPayment.Responses;

namespace Vennderful.Application.Features.EventPayment.Requests
{
    public  class CreateEventPaymentCommand : IRequest<CreateEventPaymentResponse>
    {
        public CreateEventPaymentDTO CreateEventPaymentDTO { get; set; }
    }
}
