using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueProfile.Requests;
using Vennderful.Application.Features.VenueProfile.Responses;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Application.Features.WorkingHours.DTOs;
using Vennderful.Application.Features.AnotherSocialProfile.DTOs;

namespace Vennderful.Application.Features.VenueProfile.Handlers.Queries
{
    public class GetVenueProfilePreviewByCompanyIdHandler : IRequestHandler<GetVenueProfilePreviewByCompanyIdRequest, GetVenueProfilePreviewByCompanyIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetVenueProfilePreviewByCompanyIdHandler> _logger;

        public GetVenueProfilePreviewByCompanyIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetVenueProfilePreviewByCompanyIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<GetVenueProfilePreviewByCompanyIdResponse> Handle(GetVenueProfilePreviewByCompanyIdRequest request, CancellationToken cancellationToken)
        {
            var venueProfile = await _unitOfWork.VenuePublicProfileRepository.GetProfileByCompanyId(request.CompanyId);
            var response = new GetVenueProfilePreviewByCompanyIdResponse();
            if (venueProfile == null)
            {
                response.Success = false;
                response.Message = "Venue Profile Not Found.";
                return response;
            }
            var venue = await _unitOfWork.VenueAccountInformationRepository.GetById(request.CompanyId);
            if (venue != null)
            {
                var socialLinks = await _unitOfWork.socialProfileRepository.GetSocialProfilesByCompany(request.CompanyId);
                var venueProfilePreview = new GetVenueProfilePreviewByCompanyIdResponseDTO()
                {
                    ProfilePictureUrl = venueProfile.ProfilePictureUrl,
                    CoverPhoto = venueProfile.CoverPhotoUrl,
                    WorkingHoursMode = venueProfile.WorkingHoursMode,
                    WorkingHours = _mapper.Map<CreateWorkingHourDto>(venueProfile.WorkingHour),
                    SocialProfile = _mapper.Map<List<CreateSocialProfileDto>>(socialLinks),
                    AboutUs = venueProfile.ProfileDescription,
                    Name = venue.CompanyName,
                    CompanyWebsite = venue.Website,
                    Phone = venue.PhoneNumber,
                    Address = venue.Address,
                };

                response.Success = true;
                response.Message = "Data fetched successfully.";
                response.Data = venueProfilePreview;
                return response;
            }
            response.Success = false;
            response.Message = "Venue Profile Detail Not Found.";
            return response;
        }
    }
}
