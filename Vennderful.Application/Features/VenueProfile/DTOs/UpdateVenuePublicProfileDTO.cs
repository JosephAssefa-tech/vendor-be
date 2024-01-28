using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.AnotherSocialProfile.DTOs;
using Vennderful.Application.Features.WorkingHours.DTOs;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.VenueProfile.DTOs
{
    public class UpdateVenuePublicProfileDTO
    {
        public string? ProfilePictureUrl { get; set; }
        public string? CoverPhotoUrl { get; set; }
        public string WorkingHoursMode { get; set; }
        public CreateWorkingHourDto? WorkingHours { get; set; }
        public List<CreateSocialProfileDto>? socialProfile { get; set; }
        public string? ProfileDescription { get; set; }
        public Guid VenueAccountInformationId { get; set; }

        public UpdateVenuePublicProfileDTO()
        {
            
        }

        public UpdateVenuePublicProfileDTO (CurateVenuePublicProfileDto venueProfile) 
        {
            if (venueProfile == null)
                return;
            ProfileDescription = venueProfile.ProfileDescription;
            CoverPhotoUrl = venueProfile.CoverPhotoUrl;
            ProfilePictureUrl = venueProfile.ProfilePictureUrl;
            WorkingHoursMode = venueProfile.WorkingHoursMode;
            WorkingHours = venueProfile.WorkingHours;
            socialProfile = venueProfile.socialProfile;
            ProfileDescription = venueProfile.ProfileDescription;
            VenueAccountInformationId = venueProfile.VenueAccountInformationId;
        }
    }
}
