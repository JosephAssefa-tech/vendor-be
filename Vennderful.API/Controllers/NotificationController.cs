using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Application.Features.Notifications.Requests;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("notifications/{Id}/read", Name = ApiActions.UpdateNotification)]
        public async Task<ActionResult<UpdateEventResponse>> UpdateNotification([Required] Guid Id)
        {
            var command = new UpdateNotificationCommand { Id = Id };
            var result = await _mediator.Send(command);

            if (result?.Errors != null && result?.Errors.Count() > 0)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("notifications/{userId}", Name = ApiActions.GetNotification)]
        public async Task<ActionResult<GetEventResponse>> GetNotification([Required]Guid userId)
        {
            var result = await _mediator.Send(new GetNotificationsRequest { UserId = userId });
            
            return Ok(result);
        }
    }
}
