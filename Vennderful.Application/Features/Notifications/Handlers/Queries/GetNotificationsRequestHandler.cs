using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.Notifications.Requests;
using Vennderful.Application.Features.Notifications.Responses;
using Vennderful.Application.Features.Notifications.Dto;
using System.Collections.Generic;

namespace Vennderful.Application.Features.Notifications.Handlers.Queries
{
    public class GetNotificationsRequestHandler : IRequestHandler<GetNotificationsRequest, GetNotificationsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNotificationsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetNotificationsResponse> Handle(GetNotificationsRequest request, CancellationToken cancellationToken)
        {
            var notifications = await _unitOfWork.notificationRepository.GetNotificationsByUserId(request.UserId);
            var response = new GetNotificationsResponse();
            if (notifications == null)
            {
                response.Success = false;
                response.Message = "Any Notification Not Found.";
                return response;
            }
            response.Success = true;
            response.Data = _mapper.Map<List<ListNotificationDTO>>(notifications);
            return response;
        }
    }
}
