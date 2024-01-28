using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Vennderful.Application.Features.EventDocuments.Dto;
using Vennderful.Application.Features.EventDocuments.Requests;
using Vennderful.Application.Features.EventDocuments.Responses;
using Vennderful.Application.Features.EventFinance.Dto;
using Vennderful.Application.Features.EventFinance.Requests;
using Vennderful.Application.Features.EventFinance.Responses;
using Vennderful.Application.Features.EventPayment.DTOs;
using Vennderful.Application.Features.EventPayment.Requests;
using Vennderful.Application.Features.EventPayment.Responses;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{companyId}/events", Name = ApiActions.GetEvents)]
        public async Task<ActionResult<GetEventsResponse>> GetEvents(string companyId)
        {
            var results = await _mediator.Send(new GetEventsRequest { CompanyId = Guid.Parse(companyId) });
            return Ok(results);
        }

        [HttpGet("{companyId}/event/{id}", Name = ApiActions.GetEvent)]
        [ProducesResponseType(typeof(EventDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetEventResponse>> GetEvent(Guid id, string companyId)
        {
            var result = await _mediator.Send(new GetEventByIdRequest { Id = id, CompanyId = Guid.Parse(companyId) });
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost("{companyId}/event", Name = ApiActions.CreateEvent)]
        public async Task<ActionResult<CreateEventResponse>> CreateEvent([FromBody] CreateEventDTO eventDto, string companyId)
        {
            eventDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateEventCommand { CreateEventDTO = eventDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/event/{result.Data.Id}", UriKind.Relative),
                result.Data);
        }
        [HttpDelete("{companyId}/event/{id}", Name = ApiActions.DeleteEvent)]
        public async Task<ActionResult<UpdateEventResponse>> DeleteEvent(Guid id, string companyId)
        {
            var command = new DeleteEventCommand { Id = id, companyId = Guid.Parse(companyId) };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("{companyId}/event", Name = ApiActions.UpdateEvent)]
        public async Task<ActionResult<UpdateEventResponse>> UpdateEvent([FromBody] UpdateEventDto eventDto)
        {
            var command = new UpdateEventCommand { UpdateEventDto = eventDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{companyId}/event/edit", Name = ApiActions.EditEvent)]
        public async Task<ActionResult<EditEventResponse>> EditEvent([FromBody] EditEventRequestDTO editDto)
        {
            var command = new EditEventCommand { Data = editDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/event/{editDto.Id}", UriKind.Relative),
                result.Data);
        }

        [HttpPost("events/{eventId}/finance", Name = ApiActions.CreateEventFinance)]
        public async Task<ActionResult<CreateEventFinanceResponse>> CreateEventFinance([Required]Guid eventId, [FromBody] CreateEventFinanceDto eventFinanceDto)
        {
            var command = new CreateEventFinanceCommand { CreateEventFinanceDto = eventFinanceDto };
            var result = await _mediator.Send(command);

            if (result?.Errors != null && result?.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/events/{eventId}/{result?.Data}", UriKind.Relative),
                result?.Data);
        }

        [HttpGet("events/{eventId}/finance", Name = ApiActions.GetEventFinance)]
        public async Task<ActionResult<GetEventFinanceResponse>> GetEventFinance([Required]Guid eventId)
        {
            var results = await _mediator.Send(new GetEventFinanceRequest { EventId = eventId });
            return Ok(results);
        }
        [HttpGet("{companyId}/event/{id}/clients", Name = ApiActions.GetEventClients)]
        public async Task<ActionResult<GetEventFinanceResponse>> GetEventClients([Required] Guid id )
        {
            var results = await _mediator.Send(new GetEventClientsRequest { Id = id });
            return Ok(results);
        }


        [HttpPost("{companyId}/events/payment", Name = ApiActions.CreateEventPayment)]
        public async Task<ActionResult<CreateEventPaymentResponse>> CreateEventPayment([FromBody] CreateEventPaymentDTO eventPaymentDTO)
        {
            var command = new CreateEventPaymentCommand { CreateEventPaymentDTO = eventPaymentDTO };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/events/{result.Data}", UriKind.Relative),
                result.Data);
        }

        [HttpGet("{companyId}/events/{eventId}/{clientId}/payment", Name = ApiActions.GetEventPayment)]
        public async Task<ActionResult<GetEventPaymentByClientIdResponse>> GetEventPayment([Required] Guid eventId, [Required] Guid clientId)
        {
            var results = await _mediator.Send(new GetEventPaymentByClientIdRequest { EventId = eventId, ClientId = clientId });
            return Ok(results);
        }

        [HttpGet("{companyId}/events/{eventId}/{packageId}/{addonId}/budget-summary-items", Name = ApiActions.GetEventFinanceBudgetSummaryItem)]
        public async Task<ActionResult<GetEventBudgetSummaryItemResponse>> GetEventBudgetSummary([Required] Guid eventId, [Required] Guid packageId, [Required] Guid addonId)
        {
            var results = await _mediator.Send(new GetEventBudgetSummaryItemRequest { EventId = eventId, PackageId = packageId, AddonId = addonId }); ;
            return Ok(results);
        }

        [HttpGet("{companyId}/{eventId}/scheduledPayment", Name = ApiActions.GetEventFinanceScheduledPayments)]
        public async Task<ActionResult<GetEventPaymentSchedulesResponse>> GetEventFinanceScheduledPayments(Guid eventId)
        {
            var results = await _mediator.Send(new GetEventPaymentSchedulesRequest { EventId = eventId });
            return Ok(results);
        }

        [HttpGet("{companyId}/events/{id}/payments", Name = ApiActions.GetEventPayments)]
        public async Task<ActionResult<GetEventPaymentsResponse>> GetEventPayments([Required] Guid id)
        {
            var results = await _mediator.Send(new GetEventPaymentsRequest { EventId = id });
            return Ok(results);
        }
        [HttpPost("{companyId}/events/eventDocument", Name = ApiActions.CreateEventDocument)]
        public async Task<ActionResult<CreateEventDocumentResponse>> CreateEventDocument([FromBody] CreateEventDocumentsDto eventDocumentDto)
        {
            var command = new CreateEventDocumentCommand { CreateEventDocument = eventDocumentDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/events/{result.Data}", UriKind.Relative),
                result.Data);
        }
    }
}