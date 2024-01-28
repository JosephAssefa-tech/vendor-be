using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.User.DTOs;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Identity.Model;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RoleManager<IdentityRole> _userRoles;

        public UserProfileController(IMediator mediator, RoleManager<IdentityRole> userRoles)
        {
            _mediator = mediator;
            _userRoles = userRoles;
        }

        [HttpGet("{companyId}/invitationRole", Name = ApiActions.GetInvitationRoles)]
        public async Task<ActionResult<string[]>> GetInvitationRoles()
        {
            return _userRoles.Roles.ToList().Select(x => x.Name).ToArray();
        }

        [HttpPost("{companyId}/userInvitation", Name = ApiActions.CreateUserInvitation)]
        public async Task<ActionResult<CreateUserInvitationResponse>> CreateUserInvitation([FromBody] CreateUserInvitationDTO userInvitationDto, string companyId)
        {
            userInvitationDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateUserInvitationCommand { CreateUserInvitationDTO = userInvitationDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/userInvitation/{result.Data.Email}", UriKind.Relative),
                result.Data);
        }

        [HttpGet("{companyId}/userInvites", Name = ApiActions.GetUserInvites)]
        public async Task<ActionResult<GetUserInvitesResponse[]>> GetUserInvites(string companyId)
        {
            var result = await _mediator.Send(new GetUserInvitesRequest { CompanyId = companyId });
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }
    }
}
