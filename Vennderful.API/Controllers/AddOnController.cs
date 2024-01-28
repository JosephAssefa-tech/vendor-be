using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.AddOn.DTOs;
using Vennderful.Application.Features.AddOn.Requests;
using Vennderful.Application.Features.AddOn.Responses;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class AddOnController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddOnController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{companyId}/addon", Name = ApiActions.GetAddOns)]
        public async Task<ActionResult<GetAddOnsResponse>> GetAddOns(string companyId)
        {
            var results = await _mediator.Send(new GetAddOnsRequest { CompanyId = Guid.Parse(companyId) });
            return Ok(results);
        }


        [HttpPost("{companyId}/addon", Name = ApiActions.CreateAddOn)]
        public async Task<ActionResult<CreateAddOnResponse>> CreateAddOn([FromBody] CreateAddOnDTO addOnDto, string companyId)
        {
            addOnDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateAddOnCommand { CreateAddOnDTO = addOnDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/addon/{result.Data.AddOnName}", UriKind.Relative),
                result.Data);
        }

        

    }
}
