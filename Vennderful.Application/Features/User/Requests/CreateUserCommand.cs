using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Vennderful.Application.Features.User.DTOs;
using Vennderful.Application.Features.User.Responses;

namespace Vennderful.Application.Features.User.Requests
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public UserRegisterDto UserRegisterDto { get; set; }
    }
}
