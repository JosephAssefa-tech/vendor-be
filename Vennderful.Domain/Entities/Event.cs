using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class Event : BaseAuditableEntity
    {
        public string EventName { get; set; }
        public string EventID { get; set; }
        public TypeOfEvent TypeOfEvents { get; set; }
        public DressCode DressCodes { get; set; }
        public string CoverPhoto { get; set; }
        public DateTime EventStartDateAndTime { get; set; }
        public DateTime EventEndDateAndTime { get; set; }
        public string EventSetupTime { get; set; }
        public string Status { get; set; }
        public Guid CompanyId { get; set; }
        public Int32 NumberOfGuests { get; set; }
        public IList<EventClient> EventClients { get; set; }
        public IList<EventAndRoom> EventAndRooms { get; set; }
        // public virtual IList<Client> Clients { get; set; }
        // public virtual IList<VenueAccountInformation> VenueAccountInformation { get; set; }
        public virtual IList<Room> Room { get; set; }
       // public IList<EventAndClient> EventClients { get; set; } = new List<EventAndClient>();


        //public IList<EventRoom> EventRooms { get; set; }
        //public Guid VenueAccountInformationId { get; set; }
        //public  Guid RoomId { get; set; }

   

    }
}


