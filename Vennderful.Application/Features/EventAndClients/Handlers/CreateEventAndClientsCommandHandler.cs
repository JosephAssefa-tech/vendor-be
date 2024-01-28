//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Vennderful.Application.Contracts.Persitence;
//using Vennderful.Application.Features.EventAndClients.Dto;
//using Vennderful.Application.Features.EventAndClients.Requests;
//using Vennderful.Application.Features.EventAndClients.Responses;
//using Vennderful.Application.Features.EventAndClients.Validators;
//using Vennderful.Application.Features.EventAndRooms.Dto;
//using Vennderful.Domain.Entities;

//namespace Vennderful.Application.Features.EventAndClients.Handlers
//{
//    public class CreateEventAndClientsCommandHandler : IRequestHandler<CreateEventAndClientsCommand, CreateEventAndClientsResponse>

//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        public CreateEventAndClientsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }
//        public async Task<CreateEventAndClientsResponse> Handle(CreateEventAndClientsCommand request, CancellationToken cancellationToken)
//        {
//            var validator = new CreateEventAndClientsDtoValidator();
//            var validationResult = await validator.ValidateAsync(request.CreateEventAndClientsDto);

//            var response = new CreateEventAndClientsResponse();

//            if (!validationResult.IsValid)
//            {
//                response.Success = false;
//                response.Message = "Creation Failed.";
//                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

//                return response;
//            }

//            var eventId = request.CreateEventAndClientsDto.EventId;
//            var clientIds = request.CreateEventAndClientsDto.ClientId;

//            // Create EventAndClient entities and associate them with the event
//            var eventAndClients = new EventAndClient
//            {
//                EventId = eventId,
//                CompanyId = request.CreateEventAndClientsDto.CompanyId,
//                ClientId = clientIds
//            };

//            eventAndClients = await _unitOfWork.EventAndClientRepository.AddAsync(eventAndClients);
//            await _unitOfWork.Save();

//            response.Success = true;
//            response.Message = "Created Successfully.";
//            response.Data = _mapper.Map<CreateEventAndClientsDto>(eventAndClients);

//            return response;
//        }
//    }
//}
