using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Responses;

namespace Vennderful.Application.Features.Events.Requests
{
    public class EditEventCommand : IRequest<EditEventResponse>
    {
        public EditEventRequestDTO Data { get; set; }
    }
}
