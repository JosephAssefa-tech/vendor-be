using System;
using System.Collections.Generic;
using System.Text;

namespace Vennderful.Application.Features.Events.DTOs
{
    public class EditEventRequestDTO
    {
        public Guid Id { get; set; }
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
    }
}
