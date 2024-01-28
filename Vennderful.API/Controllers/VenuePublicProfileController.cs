using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Application.Features.VenueAccount.Requests;
using Vennderful.Application.Features.VenueAccount.Responses;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Application.Features.VenueProfile.Requests;
using Vennderful.Application.Features.VenueProfile.Responses;
namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class VenuePublicProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public VenuePublicProfileController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{companyId}/CurateVenuePublicProfile", Name = ApiActions.CurateVenuePublicProfile)]
        public async Task<ActionResult<CurateVenuePublicProfileResponse>> CurateVenuePublicProfile([FromBody] CurateVenuePublicProfileDto venuePublicProfileDto, string companyId)
        {
            venuePublicProfileDto.VenueAccountInformationId = Guid.Parse(companyId);

            var existingVenueProfile = await _unitOfWork.VenuePublicProfileRepository.GetProfileByCompanyId(Guid.Parse(companyId));
            if (existingVenueProfile != null)
            {
                var command = new UpdateVenuePublicProfileCommand { UpdateVenuePublicProfileDTO = new UpdateVenuePublicProfileDTO(venuePublicProfileDto) };
                var result = await _mediator.Send(command);

                if (result.Errors != null && result.Errors.Count() > 0)
                    return BadRequest(result);
                return Created(new Uri($"/VenueAccountInformation/{result.Data.VenueAccountInformationId}", UriKind.Relative),
                    result.Data);
            }
            else
            {
                var command = new CurateVenuePublicProfileCommand { CurateVenuePublicProfileDto = venuePublicProfileDto };
                var result = await _mediator.Send(command);

                if (result.Errors != null && result.Errors.Count() > 0)
                    return BadRequest(result);
                return Created(new Uri($"/VenuePublicProfile/{result.Data.VenueAccountInformationId}", UriKind.Relative),
                    result.Data);
            }
        }

        [HttpPut("{companyId}/updateVenueProfile", Name = ApiActions.UpdateVenueProfile)]
        public async Task<ActionResult<CompleteVenueCreationResponse>> UpdateVenueProfile(UpdateVenuePublicProfileDTO updateVenuePublicProfileDTO, string companyId)
        {
            // read logged in user from token
            updateVenuePublicProfileDTO.VenueAccountInformationId = Guid.Parse(companyId);
            var command = new UpdateVenuePublicProfileCommand { UpdateVenuePublicProfileDTO = updateVenuePublicProfileDTO };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/VenueAccountInformation/{result.Data.VenueAccountInformationId}", UriKind.Relative),
                result.Data);

        }
        [HttpGet("{companyId}/GetVenueProfileDetail")]
        public async Task<ActionResult<GetVenueProfilePreviewByCompanyIdResponse>> Get(string companyId)
        {
            var result = await _mediator.Send(new GetVenueProfilePreviewByCompanyIdRequest { CompanyId = Guid.Parse(companyId) });

            return Ok(result);
        }
    }
}
