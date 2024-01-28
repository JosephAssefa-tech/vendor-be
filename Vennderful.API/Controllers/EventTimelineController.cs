using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Vennderful.Application.Features.EventTimeline.DTOs;
using Vennderful.Application.Features.EventTimeline.Requests;
using Vennderful.Application.Features.EventTimeline.Responses;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class EventTimelineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventTimelineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("events/{eventId}/timeline", Name = ApiActions.CreateEventTimeline)]
        public async Task<ActionResult<CreateEventTimelineResponse>> CreateEventTimeline([Required] Guid eventId, [FromBody] CreateEventTimelineDto eventTimeineDto)
        {
            var command = new CreateEventTimelineCommand { CreateEventTimelineDto = eventTimeineDto, EventId = eventId };
            var result = await _mediator.Send(command);

            if (result?.Errors != null && result?.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/events/{eventId}/{result?.Data}", UriKind.Relative),
                result?.Data);
        }

        [HttpPut("events/{eventId}/timeline/{id}", Name = ApiActions.UpdateEventTimeline)]
        public async Task<ActionResult<UpdateEventTimelineResponse>> UpdateEventTimeline([FromBody] UpdateEventTimelineDto timelineDto)
        {
            var command = new UpdateEventTimelineCommand { UpdateEventTimelineDto = timelineDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("events/{eventId}/timeline", Name = ApiActions.GetEventTimelines)]
        public async Task<ActionResult<GetEventTimelinesResponse>> GetEventTimelines([Required] Guid eventId)
        {
            var results = await _mediator.Send(new GetEventTimelinesRequest { EventId = eventId });
            return Ok(results);
        }

        [HttpDelete("events/{eventId}/timeline/{id}", Name = ApiActions.DeleteEventTimeline)]
        public async Task<ActionResult<DeleteEventTimelineResponse>> DeleteEventTimeline(Guid id)
        {
            var command = new DeleteEventTimelineCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
