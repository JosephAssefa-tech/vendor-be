﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventRoom.Dto;
using Vennderful.Application.Features.EventRoom.Responses;



namespace Vennderful.Application.Features.EventRoom.Requests
{
    public class CreateRoomCommand: IRequest<CreateRoomResponse>
    {
        public CreateRoomDto CreateRoomDto { get; set; }
    }
}
