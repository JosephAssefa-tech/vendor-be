using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.NewDocuments.DTOs;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;
using Vennderful.Application.Features.NewDocuments.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.NewDocuments.Handlers.Commands
{
    public class CreateEventDocumentSignatureHandler: IRequestHandler<CreateEventDocumentSignatureNotificationCommand, CreateEventDocumentSignatureNotificationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
    public CreateEventDocumentSignatureHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }

        public async Task<CreateEventDocumentSignatureNotificationResponse> Handle(CreateEventDocumentSignatureNotificationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventDocumentSignatureDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateEventDocumentSignatureDto);

            var response = new CreateEventDocumentSignatureNotificationResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }
            var notification = _mapper.Map<Notification>(request.CreateEventDocumentSignatureDto);
            notification = await _unitOfWork.notificationRepository.AddAsync(notification);

            await _unitOfWork.Save();


            //var clientIds = request.CreateEventDocumentSignatureDto.ClientId;

            //foreach (var clientId in clientIds)
            //{
            //    var dto = new CreateEventDocumentSignatureDto
            //    {
            //        UserId = request.CreateEventDocumentSignatureDto.UserId,
            //        NotificationType = request.CreateEventDocumentSignatureDto.NotificationType,
            //        NotificationMethod = request.CreateEventDocumentSignatureDto.NotificationMethod,
            //        Content = request.CreateEventDocumentSignatureDto.Content,
            //        ClientId = new List<Guid?> { clientId },
            //        EventId = request.CreateEventDocumentSignatureDto.EventId,
            //        EventDocumentId = request.CreateEventDocumentSignatureDto.EventDocumentId,
            //        DocumentId = request.CreateEventDocumentSignatureDto.DocumentId,
            //        SenderId = request.CreateEventDocumentSignatureDto.SenderId,
            //        HasBeenRead = request.CreateEventDocumentSignatureDto.HasBeenRead
            //    };

            //    var notification = _mapper.Map<Notification>(dto);
            //    notification = await _unitOfWork.notificationRepository.AddAsync(notification);
            //}

            await _unitOfWork.Save();

            // Return the appropriate response
            response.Success = true;
            response.Message = "Creation Successful.";
            return response;
        }

    }
}

