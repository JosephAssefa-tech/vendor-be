using MediatR;
using System;
using Vennderful.Application.Features.Notifications.Responses;

namespace Vennderful.Application.Features.Notifications.Requests
{
    public class GetNotificationsRequest : IRequest<GetNotificationsResponse>
    {
        public Guid UserId { get; set; }
    }
}
