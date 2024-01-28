using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.UserRoles.DTOs;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.UserRoles.Responses
{
    public class AddUserRoleResponse : BaseResponse
    {
        public AddUserRoleDTO Data { get; set; }
    }
}
