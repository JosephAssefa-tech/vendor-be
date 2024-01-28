using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.PackageCategories.DTOs;
using Vennderful.Application.Features.PackageCategories.Requests;
using Vennderful.Application.Features.PackageCategories.Responses;
using Vennderful.Domain.Entities;

namespace Vennderful.API.Controllers
{

    [Route("")]
    [ApiController]
    public class PackageCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PackageCategoryController(IMediator mediator)
        {
            _mediator = mediator;

        }


        [HttpGet("{companyId}/PackageCategoryList")]
        public async Task<ActionResult<PackageCategory>> GetAllPackage([FromQuery] GetPackageCategoriesRequest param)
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
