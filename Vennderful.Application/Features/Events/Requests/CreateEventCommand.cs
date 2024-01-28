using MediatR;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Application.Models.Mail;

namespace Vennderful.Application.Features.Events.Requests
{
    public class CreateEventCommand : IRequest<CreateEventResponse>
    {
        public CreateEventDTO CreateEventDTO { get; set; }
    }
}
