using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.EventRoom.Dto;
using Vennderful.Application.Features.EventRoom.Requests;
using Vennderful.Application.Features.EventRoom.Responses;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{companyId}/room", Name = ApiActions.GetRoom)]
        public async Task<ActionResult<GetRoomResponse>> GetRooms(string companyId)
        {
            var results = await _mediator.Send(new GetRoomsRequest { CompanyId = Guid.Parse(companyId) });
            return Ok(results);
        }

        [HttpPost("{companyId}/room", Name = ApiActions.CreateRoom)]
        public async Task<ActionResult<CreateRoomResponse>> CreateRoom([FromBody] CreateRoomDto roomDto, string companyId)
        {
            roomDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateRoomCommand { CreateRoomDto = roomDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Any()) // Check if errors are not empty
                return BadRequest(result);
            if (result.Data == null) // Check if result.Data is null
                return Conflict("Room with the same name already exists."); // Return a 409 Conflict status with the specific message

            return Created(new Uri($"/room/{result.Data.RoomName}", UriKind.Relative), result.Data);
        }


    }
}
