using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Responses;

namespace Vennderful.Application.Features.Events.Requests
{
    public class UpdateEventCommand: IRequest<UpdateEventResponse>
    {
        public UpdateEventDto UpdateEventDto { get; set; }
        //public Guid Id { get; set; }
       // public Guid companyId { get; set; }
    }
}
