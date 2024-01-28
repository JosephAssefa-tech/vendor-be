using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;
using Vennderful.Application.Features.EventAndMember.DTO;
using Vennderful.Application.Features.EventAndMember.Requests;
using Vennderful.Application.Features.EventAndMember.Responses;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Member.DTO;
using Vennderful.Application.Features.Member.Requests;
using Vennderful.Application.Features.Member.Responses;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{companyId}/members")]
        public async Task<GetMembersResponse> GetMembers(string companyId)
        {
            var results = await _mediator.Send(new GetMembersRequest { CompanyId = Guid.Parse(companyId) });
            return results;
        }

        [HttpGet("{companyId}/members/{eventId}")]
        public async Task<GetEventAndMembersResponse> GetMembersByEventId(Guid eventId)
        {
            var results = await _mediator.Send(new GetEventAndMembersRequest { EventId = eventId });
            return results;
        }
        [HttpPost("{companyId}/members/event")]
        public async Task<ActionResult<CreateEventAndMemberResponse>> CreateEventAndMember([FromBody] CreateEventAndMemberDTO createEventAndMemberDTO)
        {
            var command = new CreateEventAndMemberCommand { CreateEventAndMemberDTO = createEventAndMemberDTO};
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/eventAndMember/{result.Data.EventId}", UriKind.Relative),
                result.Data);
        }
    }
}
