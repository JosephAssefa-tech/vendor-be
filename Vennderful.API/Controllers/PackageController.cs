using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.Package.DTOs;
using Vennderful.Application.Features.Package.Requests;
using Vennderful.Application.Features.Package.Responses;

namespace Vennderful.API.Controllers
{

    [Route("")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{companyId}/package", Name = ApiActions.CreatePackage)]
        public async Task<ActionResult<CreatePackageResponse>> CreatePackage([FromBody] CreatePackageDTO packageDto, string companyId)
        {
            packageDto.CompanyId = Guid.Parse(companyId);
            var command = new CreatePackageCommand { CreatePackageDTO = packageDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/package/{result.Data.PackageName}", UriKind.Relative),
                result.Data);
        }

        [HttpGet("{companyId}/package", Name = ApiActions.GetPackages)]
        public async Task<ActionResult<GetPackagesResponse>> GetPackages(string companyId)
        {
            var results = await _mediator.Send(new GetPackagesRequest { CompanyId = Guid.Parse(companyId)});
            return Ok(results);
        }




    }
}
