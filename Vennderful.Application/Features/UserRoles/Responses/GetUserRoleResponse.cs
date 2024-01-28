using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.UserRoles.DTOs;

namespace Vennderful.Application.Features.UserRoles.Responses
{
    public class GetUserRoleResponse : BaseResponse
    {
        public UserRoleDTO Data { get; set; }
    }
}
