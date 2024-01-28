using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Application.Features.VenueAccount.Requests;
using Vennderful.Application.Features.VenueAccount.Responses;


namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VenueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/<VenueController>
        [HttpPost("{companyId}/Venue", Name = ApiActions.CreateVenueAccountInformation)]
        public async Task<ActionResult<CreateVenueAccountInformationResponse>> CreateVenueAccount([FromBody] CreateVenueAccountInformationDto VenueDto, string companyId)
        {
            VenueDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateVenueAccountInformationCommand { CreateVenueAccountInformationDto = VenueDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/VenueAccountInformation/{result.Data.Id}", UriKind.Relative),
                result.Data);
        }

        [HttpPut("{companyId}/completeVenueProfile", Name = ApiActions.CompleteVenueProfile)]
        public async Task<ActionResult<CompleteVenueCreationResponse>> CompleteVenueCreation(CompleteVenueCreationDTO completeVenueCreationDTO, string companyId)
        {
            completeVenueCreationDTO.CompanyId =Guid.Parse(companyId);
            var command = new CompleteVenueCreationCommand { CompleteVenueCreationDTO = completeVenueCreationDTO };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/VenueAccountInformation/{result.Data.CompanyName}", UriKind.Relative),
                result.Data);

        }

        [HttpGet("{companyId}/GetVenueProfile")]
        public async Task<ActionResult<VenueAccountInformationDto>> Get(string companyId)
        {
            var result = await _mediator.Send(new GetVenueAccountInformationByIdQuery { CompanyId = Guid.Parse(companyId) });

            return Ok(result);
        }
    }
}
