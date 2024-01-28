
using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.VenueAccount.DTOs;

namespace Vennderful.Application.Features.Events.Responses
{
    public class GetEventsResponse : BaseResponse
    {
        public List<ListEventDTO> Data { get; set; }
        public VenueAccountInformationDto VenueAccountInformation { get; set; }
    }
}
