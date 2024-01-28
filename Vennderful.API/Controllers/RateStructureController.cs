using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.RateStructure.Requests;
using Vennderful.Application.Features.RateStructure.Responses;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class RateStructureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RateStructureController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{companyId}/rate-structure", Name = ApiActions.GetRateStructures)]
        public async Task<ActionResult<List<GetRateStructuresResponse>>> GetRateStructures()
        {
            var results = await _mediator.Send(new GetRateStructuresRequest());
            return Ok(results);
        }
    }
}
