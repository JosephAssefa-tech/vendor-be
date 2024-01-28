using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.VenueAccount.DTOs
{
    public class CompleteVenueCreationDTO
    {
        public string CompanyName { get; set; }
        public CompanyProfileStatus Status { get; set; }
        public Guid CompanyId { get; set; }
    }
}
