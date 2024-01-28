using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Application.Features.VenueAccount.Requests;
using Vennderful.Application.Features.VenueAccount.Responses;
using Vennderful.Application.Features.VenueAccount.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.VenueAccount.Handlers.Commands
{
    public class CreateVenueAccountInformationHandler : IRequestHandler<CreateVenueAccountInformationCommand, CreateVenueAccountInformationResponse>
    {
     
             private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateVenueAccountInformationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateVenueAccountInformationResponse> Handle(CreateVenueAccountInformationCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateVenueAccountInformationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateVenueAccountInformationDto);

            var response = new CreateVenueAccountInformationResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }
            var existingVenue = await _unitOfWork.VenueAccountInformationRepository.GetVenueByCompanyName(request.CreateVenueAccountInformationDto.CompanyName, request.CreateVenueAccountInformationDto.CompanyId);
            var venue = _mapper.Map<VenueAccountInformation>(request.CreateVenueAccountInformationDto);
            venue.Status = Domain.Enums.CompanyProfileStatus.Pending;
            if (existingVenue == null)
            {                
                venue = await _unitOfWork.VenueAccountInformationRepository.AddAsync(venue);
            }
            else
            {
                existingVenue.CompanyName = venue.CompanyName;
                existingVenue.PhoneNumber = venue.PhoneNumber;
                existingVenue.Address = venue.Address;
                existingVenue.TypeOfBusinessId = venue.TypeOfBusinessId;
                existingVenue.TypeOfBusiness = venue.TypeOfBusiness;
                existingVenue.Website = venue.Website;
                await _unitOfWork.VenueAccountInformationRepository.UpdateAsync(existingVenue);
            }

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateVenueAccountInformationDto>(venue);

            return response;
        }
    }
}
