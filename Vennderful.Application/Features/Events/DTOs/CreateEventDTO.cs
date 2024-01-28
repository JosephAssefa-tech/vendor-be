
using System;
using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.Events.DTOs
{
    public class CreateEventDTO : BaseDTO
    {
        public string? EventName { get; set; }
        public string? EventID { get; set; }
        public TypeOfEvent? TypeOfEvents { get; set; }
        public Int32? NumberOfGuests { get; set; }
        public DressCode? DressCodes { get; set; }
        public string? CoverPhoto { get; set; }
        public string? EventSetupTime { get; set; }
        public DateTime? EventStartDateAndTime { get; set; }
        public DateTime? EventEndDateAndTime { get; set; }
        public string? Status { get; set; }
        public Guid CompanyId { get; set; }
       // public virtual IList<ListClientDTO> Clients { get; set; }


    }
}
