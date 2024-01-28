using System;
using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.Events.DTOs
{
    public class EventDTO : BaseDTO
    {
        public string EventName { get; set; }
        public string EventID { get; set; }
        public string EventType { get; set; }
        public string DressCode { get; set; }
        public string CoverPhoto { get; set; }
        public string EventStartDate { get; set; }
        public string EventStartTime { get; set; }
        public string EventEndDate { get; set; }
        public string EventEndTime { get; set; }
        public string EventSetupTime { get; set; }
        public string EventRoom { get; set; }
        public Guid CompanyId { get; set; }
        public Int32 NumberOfGuests { get; set; }
        public string Venue { get; set; }
        public string EventLocation { get; set; }
        public string Status { get; set; }
        public virtual IList<ListClientDTO> Clients { get; set; }
    }
}
