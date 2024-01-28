using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Notifications.Dto;

namespace Vennderful.Application.Features.Notifications.Responses
{
    public class GetNotificationsResponse : BaseResponse
    {
        public List<ListNotificationDTO> Data { get; set; }
    }
}
