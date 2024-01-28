using Vennderful.Application.Common;
using Vennderful.Application.Features.EventPayment.DTOs;

namespace Vennderful.Application.Features.EventPayment.Responses
{
    public class CreateEventPaymentResponse : BaseResponse
    {
        public CreateEventPaymentDTO Data { get; set; }
    }
}
