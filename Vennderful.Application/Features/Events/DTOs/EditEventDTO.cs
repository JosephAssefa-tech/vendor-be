using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.Events.DTOs
{
    public  class EditEventDTO : BaseDTO
    {
        public string EventName { get; set; }
        public string EventID { get; set; }
        public string TypeOfEvents { get; set; }
        public Int32 NumberOfGuests { get; set; }
        public string DressCodes { get; set; }
        public string CoverPhoto { get; set; }
        public string EventSetupTime { get; set; }
        public List<string> EventRooms { get; set; }
        public DateTime EventStartDateAndTime { get; set; }
        public DateTime EventEndDateAndTime { get; set; }

        public string Venue { get; set; }
        public string EventLocation { get; set; }
        public string Status { get; set; }
        public Guid CompanyId { get; set; }
    }
}
