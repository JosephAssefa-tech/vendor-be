using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Vennderful.Application.Features.Notifications.Responses;

namespace Vennderful.Application.Features.Notifications.Requests
{
    public class UpdateNotificationCommand : IRequest<UpdateNotificationResponse>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
