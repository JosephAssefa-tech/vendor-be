using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Application.Features.VenueProfile.Requests;
using Vennderful.Application.Features.VenueProfile.Responses;
using Vennderful.Application.Features.VenueProfile.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.VenueProfile.Handlers.Commands
{
    public class CurateVenuePublicProfileHandler : IRequestHandler<CurateVenuePublicProfileCommand, CurateVenuePublicProfileResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurateVenuePublicProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CurateVenuePublicProfileResponse> Handle(CurateVenuePublicProfileCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateVenuePublicProfileDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CurateVenuePublicProfileDto);

            var response = new CurateVenuePublicProfileResponse();
            var existingVenueProfile = await _unitOfWork.VenuePublicProfileRepository.GetProfileByCompanyId(request.CurateVenuePublicProfileDto.VenueAccountInformationId);

            if (!validationResult.IsValid || existingVenueProfile != null)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors != null ? validationResult.Errors.Select(e => e.ErrorMessage).ToList() : new List<string>() { "Profile already exist for given company"};

                return response;
            }
            WorkingHour workingHour = null;
            List<SocialProfile> socialProfileList = null;
            if (request.CurateVenuePublicProfileDto.WorkingHours != null)
            {
                workingHour = _mapper.Map<WorkingHour>(request.CurateVenuePublicProfileDto.WorkingHours);
                workingHour.Id = Guid.NewGuid();
                workingHour.Created = DateTime.UtcNow;
                workingHour.CreatedBy = "TokenUser";
                workingHour = await _unitOfWork.workingHoursRepository.AddAsync(workingHour);
            }
            if (request.CurateVenuePublicProfileDto.socialProfile != null && request.CurateVenuePublicProfileDto.socialProfile.Count > 0)
            {
                socialProfileList = _mapper.Map<List<SocialProfile>>(request.CurateVenuePublicProfileDto.socialProfile);
                foreach (var profile in socialProfileList)
                {
                    if (!string.IsNullOrEmpty(profile.SocialProfileName))
                    {
                        profile.Id = Guid.NewGuid();
                        profile.Created = DateTime.UtcNow;
                        profile.CreatedBy = "TokenUser";
                        profile.CompanyId = request.CurateVenuePublicProfileDto.VenueAccountInformationId;
                        _ = await _unitOfWork.socialProfileRepository.AddAsync(profile);
                    }
                }
            }

            var venue = _mapper.Map<VenuePublicProfile>(request.CurateVenuePublicProfileDto);



            venue.Id = Guid.NewGuid();
            venue.CreatedBy = "TokenUser";
            venue.Created = DateTime.UtcNow;
            venue.WorkingHourId = workingHour?.Id;
            venue.WorkingHour = workingHour;
            venue.VenueAccountInformationId = request.CurateVenuePublicProfileDto.VenueAccountInformationId;
            venue = await _unitOfWork.VenuePublicProfileRepository.AddAsync(venue);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CurateVenuePublicProfileDto>(venue);

            return response;
        }
    }
}
