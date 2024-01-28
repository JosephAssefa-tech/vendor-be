using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vennderful.Domain.Common;
using Vennderful.Domain.ValueObjects;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class VenuePublicProfile : BaseAuditableEntity
    {
        public string? ProfilePictureUrl { get; set; }
        public string? CoverPhotoUrl { get; set; }
        public string WorkingHoursMode { get; set; }
        public virtual Guid? WorkingHourId { get; set; }
        public string? ProfileDescription { get; set; }
        public Guid VenueAccountInformationId { get; set; }
        public virtual WorkingHour WorkingHour { get; set; }
    }
}
