using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Domain.Entities
{
    public class VenueAccountInformation : BaseAuditableEntity
    {
        public string CompanyName { get; set; }
        public string? Website { get; set; }
        public string PhoneNumber { get; set; }

        [Display(Name = "TypeOfBusiness")]
        public virtual Guid TypeOfBusinessId { get; set; }
        [ForeignKey("TypeOfBusinessId")]
        public virtual TypeOfBusiness TypeOfBusiness { get;set; }
        public Address Address { get; set; }
        public CompanyProfileStatus Status { get; set; }
        public Guid CompanyId { get; set; }
    }
}
