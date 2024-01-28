using System;
using System.Collections.Generic;
using Vennderful.Application.Features.AnotherSocialProfile.DTOs;
using Vennderful.Application.Features.WorkingHours.DTOs;

namespace Vennderful.Application.Features.VenueProfile.DTOs
{
    public class CurateVenuePublicProfileDto 
    {
        public string? ProfilePictureUrl { get; set; }
        public string? CoverPhotoUrl { get; set; }
        public string WorkingHoursMode { get; set; }
        public CreateWorkingHourDto? WorkingHours { get; set; }
        public List<CreateSocialProfileDto>? socialProfile { get; set; }
        public string? ProfileDescription { get; set; }
        public Guid VenueAccountInformationId { get; set; }

    }
}
