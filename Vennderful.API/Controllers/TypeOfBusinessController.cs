using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.TypeOfBusiness.Requests;
using Vennderful.Application.Features.TypeOfBusiness.Responses;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class TypeOfBusinessController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TypeOfBusinessController(IMediator mediator)
        {
            _mediator = mediator;

        }
        // GET: api/<TypeOfBusinessController>
        [HttpGet]
        [HttpGet("{companyId}/TypesOfBusiness", Name = ApiActions.GetTypesOfBusiness)]
        public async Task<ActionResult<GetTypeOfBusinessResponse>> GetTypesOfBusiness()
        {
            var results = await _mediator.Send(new GetTypesOfBusinessRequest());
                return Ok(results);
        }

    }
}
