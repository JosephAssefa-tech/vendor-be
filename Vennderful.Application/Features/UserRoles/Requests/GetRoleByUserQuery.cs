using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.UserRoles.Responses;

namespace Vennderful.Application.Features.UserRoles.Requests
{
    public class GetRoleByUserQuery: IRequest<GetUserRoleResponse>
    {
        public Guid CompanyId { get; set; }

    }
}
