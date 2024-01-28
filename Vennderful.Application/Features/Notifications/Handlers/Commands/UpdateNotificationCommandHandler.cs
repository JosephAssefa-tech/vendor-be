using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Vennderful.Application.Features.Notifications.Requests;
using Vennderful.Application.Features.Notifications.Responses;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;

namespace Vennderful.Application.Features.Notifications.Handlers.Commands
{
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, UpdateNotificationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateNotificationResponse> Handle(UpdateNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var response = new UpdateNotificationResponse();

            try
            {
                var notification = await _unitOfWork.notificationRepository.GetByIdAsync(request.Id);
                if (notification != null)
                {
                    notification.HasBeenRead = true;
                    await _unitOfWork.notificationRepository.UpdateAsync(notification);
                }

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = new List<string> { "Bad Request." };

                return response;
            }

            response.Success = true;
            response.Message = "Updated Successfully.";
            response.IsRead = true;

            return response;
        }
    }
}
