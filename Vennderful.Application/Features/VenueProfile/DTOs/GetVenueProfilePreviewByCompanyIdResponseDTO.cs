using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.AnotherSocialProfile.DTOs;
using Vennderful.Application.Features.WorkingHours.DTOs;
using Vennderful.Domain.ValueObjects;
using static AutoMapper.Internal.ExpressionFactory;

namespace Vennderful.Application.Features.VenueProfile.DTOs
{
    public class GetVenueProfilePreviewByCompanyIdResponseDTO
    {
        public string? ProfilePictureUrl { get; set; }
        public string? CoverPhoto { get; set; }
        public string WorkingHoursMode { get; set; }
        public CreateWorkingHourDto? WorkingHours { get; set; }
        public List<CreateSocialProfileDto>? SocialProfile { get; set; }
        public string AboutUs { get; set; }
        public string Name { get; set; }
        public string? CompanyWebsite { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public string VenueType { get; set; } = "Venue";
        public string Email { get; set; }
        public decimal Rating { get; set; } = 4.5M;

    }
}
