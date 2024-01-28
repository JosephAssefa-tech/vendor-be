using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.UserRoles.DTOs;
using Vennderful.Application.Features.UserRoles.Responses;
using Vennderful.Domain.Entities;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.UserRoles.Requests
{
    public class AddUserRoleCommand : IRequest<AddUserRoleResponse>
    {
        public AddUserRoleDTO AddUserRoleDTO { get; set; }
    }
}