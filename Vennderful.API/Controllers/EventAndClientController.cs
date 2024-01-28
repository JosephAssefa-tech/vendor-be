using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.EventAndClients.Dto;
using Vennderful.Application.Features.EventAndClients.Requests;
using Vennderful.Application.Features.EventAndClients.Responses;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class EventAndClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventAndClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{companyId}/eventAndClient", Name = ApiActions.CreateEventAndClient)]
        public async Task<ActionResult<CreateEventAndClientsResponse>> CreateEvent([FromBody] CreateEventAndClientsDto eventAndClientDto, string companyId)
        {
            eventAndClientDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateEventAndClientsCommand { CreateEventAndClientsDto = eventAndClientDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/event/{result.Data.Id}", UriKind.Relative),
                result.Data);
        }
    }
}
