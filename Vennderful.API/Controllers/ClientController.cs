using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;
using Vennderful.Identity.Model;
namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RoleManager<IdentityRole> _clientRoles;

        public ClientController(IMediator mediator, RoleManager<IdentityRole> clientRoles)
        {
            _mediator = mediator;
            _clientRoles = clientRoles;
        }

        [HttpGet("{companyId}/clientInvitationRole")]
        public async Task<ActionResult<string[]>> GetInvitationRoles()
        {
            return _clientRoles.Roles.ToList().Select(x => x.Name).ToArray();
        }

        [HttpPost("clients/{eventId}")]
        public async Task<ActionResult<CreateClientResponse>> CreateUserInvitation([FromBody] CreateClientDTO createClienDto, Guid eventId)
        {
            var command = new CreateClientCommand { CreateClientDTO = createClienDto, EventId = eventId };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/clinetInvitation/{result.Data.Id}", UriKind.Relative),
                result.Data);
        }

        [HttpGet("{companyId}/clientInvites")]
        public async Task<ActionResult<GetClientInvitesResponse[]>> GetClientInvites(string companyId)
        {
            var result = await _mediator.Send(new GetClientInvitesRequest { CompanyId = companyId });
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet("{companyId}/clients")]
        public async Task<ActionResult<GetClientsResponse[]>> GetClientsByName(string searchQuery, string companyId)
        {
            var result = await _mediator.Send(new GetClientsRequest { SearchQuery = searchQuery, CompanyId = companyId });
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet("{companyId}/client/{clientId}")]
        public async Task<ActionResult<GetClientResponse>> GetClient(string clientId, string companyId)
        {
            var result = await _mediator.Send(new GetClientRequest { ClientId = clientId, CompanyId = companyId });
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost("{companyId}/existingClientsInvites/{eventId}")]
        public async Task<ActionResult<CreateClientResponse>> CreateExistingClientInvitation([FromBody] CreateClientInvitationDTO createExistingClienDto, Guid eventId)
        {
            var command = new CreateClientInvitationCommand { CreateClientInvitationDTO = createExistingClienDto, EventId = eventId };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/existingClientsInvites", UriKind.Relative),
                result.Data);
        }
    }
}
