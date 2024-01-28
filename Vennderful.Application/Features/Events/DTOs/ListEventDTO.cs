
using System;
using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.VenueAccount.DTOs;

namespace Vennderful.Application.Features.Events.DTOs
{
    public class ListEventDTO : BaseDTO
    {
        public string EventName { get; set; }
        public string EventID { get; set; }
        public string CoverPhoto { get; set; }
        public DateTime EventStartDateAndTime { get; set; }
        public DateTime EventEndDateAndTime { get; set; }
        public string EventSetupTime { get; set; }

        public  IList<ListClientDTO> Client { get; set; }
        public VenueAccountInformationDto VenueAccountInformation { get; set; }

    }
}
