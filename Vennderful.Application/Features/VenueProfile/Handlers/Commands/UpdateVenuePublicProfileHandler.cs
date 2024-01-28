using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueProfile.Requests;
using Vennderful.Application.Features.VenueProfile.Responses;
using Vennderful.Application.Features.VenueProfile.Validators;
using System.Linq;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.VenueProfile.Handlers.Commands
{
    public class UpdateVenuePublicProfileHandler : IRequestHandler<UpdateVenuePublicProfileCommand, UpdateVenuePublicProfileResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVenuePublicProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateVenuePublicProfileResponse> Handle(UpdateVenuePublicProfileCommand request, CancellationToken cancellationToken)
        {

            var validator = new UpdateVenuePublicProfileDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateVenuePublicProfileDTO);

            var response = new UpdateVenuePublicProfileResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Update Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var existingVenueProfile = await _unitOfWork.VenuePublicProfileRepository.GetProfileByCompanyId(request.UpdateVenuePublicProfileDTO.VenueAccountInformationId);

            WorkingHour workingHour = null;
            List<SocialProfile> socialProfileList = null;
            if (request.UpdateVenuePublicProfileDTO.WorkingHours != null)
            {
                workingHour = _mapper.Map<WorkingHour>(request.UpdateVenuePublicProfileDTO.WorkingHours);
                if (existingVenueProfile != null)
                {
                    var existingHour = existingVenueProfile.WorkingHourId != null ? await _unitOfWork.workingHoursRepository.GetByIdAsync((Guid)existingVenueProfile.WorkingHourId) : null;
                    if (existingHour != null)
                    {
                        existingHour.LastModified = DateTime.UtcNow;
                        existingHour.LastModifiedBy = "TokenUser";
                        existingHour.MondayOpeningHour = workingHour.MondayOpeningHour;
                        existingHour.MondayClosingHour = workingHour.MondayClosingHour;
                        existingHour.TuesdayOpeningHour = workingHour.TuesdayOpeningHour;
                        existingHour.TuesdayClosingHour = workingHour.TuesdayClosingHour;
                        existingHour.WednesdayOpeningHour = workingHour.WednesdayOpeningHour;
                        existingHour.WednesdayClosingHour = workingHour.WednesdayClosingHour;
                        existingHour.ThursdayOpeningHour = workingHour.ThursdayOpeningHour;
                        existingHour.ThursdayClosingHour = workingHour.ThursdayClosingHour;
                        existingHour.FridayOpeningHour = workingHour.FridayOpeningHour;
                        existingHour.FridayClosingHour = workingHour.FridayClosingHour;
                        existingHour.SaturdayOpeningHour = workingHour.SaturdayOpeningHour;
                        existingHour.SaturdayClosingHour = workingHour.SaturdayClosingHour;
                        existingHour.SundayOpeningHour = workingHour.SundayOpeningHour;
                        existingHour.SundayClosingHour = workingHour.SundayClosingHour;
                        await _unitOfWork.workingHoursRepository.UpdateAsync(existingHour);
                        workingHour = existingHour;
                    }
                    else
                    {
                        workingHour.Id = Guid.NewGuid();
                        workingHour.Created = DateTime.UtcNow;
                        workingHour.CreatedBy = "TokenUser";
                        _ = await _unitOfWork.workingHoursRepository.AddAsync(workingHour);
                    }
                }
            }
            if (request.UpdateVenuePublicProfileDTO.socialProfile != null && request.UpdateVenuePublicProfileDTO.socialProfile.Count > 0)
            {
                socialProfileList = _mapper.Map<List<SocialProfile>>(request.UpdateVenuePublicProfileDTO.socialProfile);

                var existingAnotherSocialProfile = await _unitOfWork.socialProfileRepository.GetSocialProfilesByCompany(request.UpdateVenuePublicProfileDTO.VenueAccountInformationId);
                
                    foreach (var profile in socialProfileList)
                    {
                        var currentProfile = existingAnotherSocialProfile?.FirstOrDefault(x => x.SocialProfileName == profile.SocialProfileName);
                        if (currentProfile != null)
                        {
                            currentProfile.LastModified = DateTime.UtcNow;
                            currentProfile.LastModifiedBy = "TokenUser";
                            currentProfile.SocialProfileLink = profile.SocialProfileLink;
                            currentProfile.SocialProfileName = profile.SocialProfileName;
                            await _unitOfWork.socialProfileRepository.UpdateAsync(currentProfile);
                        }
                        else
                        {
                            profile.Id = Guid.NewGuid();
                            profile.Created = DateTime.UtcNow;
                            profile.CreatedBy = "TokenUser";
                            profile.CompanyId = request.UpdateVenuePublicProfileDTO.VenueAccountInformationId;
                            _ = await _unitOfWork.socialProfileRepository.AddAsync(profile);
                        }
                    }
            }

            var venue = _mapper.Map<VenuePublicProfile>(request.UpdateVenuePublicProfileDTO);

            if (existingVenueProfile != null)
            {
                existingVenueProfile.CoverPhotoUrl =!string.IsNullOrEmpty(venue.CoverPhotoUrl) ? venue.CoverPhotoUrl : existingVenueProfile.CoverPhotoUrl;
                existingVenueProfile.ProfilePictureUrl = !string.IsNullOrEmpty(venue.ProfilePictureUrl) ? venue.ProfilePictureUrl : existingVenueProfile.ProfilePictureUrl;
                existingVenueProfile.ProfileDescription = !string.IsNullOrEmpty(venue.ProfileDescription) ? venue.ProfileDescription : existingVenueProfile.ProfileDescription;
                existingVenueProfile.WorkingHoursMode = !string.IsNullOrEmpty(venue.WorkingHoursMode) ? venue.WorkingHoursMode : existingVenueProfile.WorkingHoursMode;
                existingVenueProfile.WorkingHourId = workingHour?.Id;
                existingVenueProfile.WorkingHour = workingHour;
                existingVenueProfile.LastModifiedBy = "TokenUser";
                existingVenueProfile.LastModified = DateTime.UtcNow;
                await _unitOfWork.VenuePublicProfileRepository.UpdateAsync(existingVenueProfile);
            }

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Updated Successfully.";
            response.Data = _mapper.Map<UpdateVenuePublicProfileDTO>(existingVenueProfile);

            return response;
        }
    }
}
