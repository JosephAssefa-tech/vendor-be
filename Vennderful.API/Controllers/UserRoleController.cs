using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.UserRoles.DTOs;
using Vennderful.Application.Features.UserRoles.Requests;
using Vennderful.Application.Features.UserRoles.Responses;

namespace Vennderful.API.Controllers
{
    public class UserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("{companyId}/Create")]
        public async Task<ActionResult<AddUserRoleResponse>> Create([FromBody] AddUserRoleDTO userROleDto)
        {
            var command = new AddUserRoleCommand { AddUserRoleDTO = userROleDto };
            var result = await _mediator.Send(command);
                
            return Ok(result);
        }
    }
}
