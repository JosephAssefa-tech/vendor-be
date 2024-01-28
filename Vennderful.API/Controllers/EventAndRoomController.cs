using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.EventAndRooms.Dto;
using Vennderful.Application.Features.EventAndRooms.Requests;
using Vennderful.Application.Features.EventAndRooms.Responses;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class EventAndRoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventAndRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{companyId}/EventAndRoom", Name = ApiActions.GetEventAndRooms)]
        public async Task<ActionResult<GetEventAndRoomsResponse>> GetListOfEventAndRooms(string companyId)
        {
            var results = await _mediator.Send(new GetEventAndRoomsRequest { CompanyId = Guid.Parse(companyId) });
            return Ok(results);
        }
        [HttpPost("{companyId}/EventAndRoom", Name = ApiActions.CreateEventAndRoom)]
        public async Task<ActionResult<CreateEventAndRoomResponse>> CreateEventAndRooms([FromBody] CreateEventAndRoomDto eventAndRoomDto, string companyId)
        {
            eventAndRoomDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateEventAndRoomsCommand { CreateEventAndRoomDto = eventAndRoomDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/EventAndRoom/{result.Data.Id}", UriKind.Relative),
                result.Data);
        }
    }
}
