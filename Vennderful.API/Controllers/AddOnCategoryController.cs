using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.AddOnsCategories.DTOs;
using Vennderful.Application.Features.AddOnsCategories.Requests;
using Vennderful.Application.Features.AddOnsCategories.Responses;
using Vennderful.Domain.Entities;

namespace Vennderful.API.Controllers
{
  
    [Route("")]
    [ApiController]
    public class AddOnCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddOnCategoryController(IMediator mediator)
        {
            _mediator = mediator;

        }
       
    
        [HttpGet("{companyId}/AddOnsCategoryList")]
        public async Task<ActionResult<AddOnCategory>> GetAllCompany([FromQuery] GetAddOnsCategoryRequest param)
        {
            var result = await _mediator.Send(param);
            if (result.Errors != null && result.Errors.Count() > 0)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

    }
}
