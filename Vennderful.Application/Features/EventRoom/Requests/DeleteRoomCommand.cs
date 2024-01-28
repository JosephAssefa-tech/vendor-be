using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventRoom.Responses;

namespace Vennderful.Application.Features.EventRoom.Requests
{
    public class DeleteRoomCommand: IRequest<DeleteRoomResponse>
    {
        public Guid Id { get; set; }
    }
}
