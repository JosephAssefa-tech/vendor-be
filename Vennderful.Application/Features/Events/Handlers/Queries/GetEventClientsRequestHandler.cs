using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.EventAndClients.Dto;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;

namespace Vennderful.Application.Features.Events.Handlers.Queries
{
    public class GetEventClientsRequestHandler : IRequestHandler<GetEventClientsRequest, GetEventClentsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventClientsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetEventClentsResponse> Handle(GetEventClientsRequest request, CancellationToken cancellationToken)
        {
            var eventClients = await _unitOfWork.eventClientRepository.GetEventClientsByEventId(request.Id);
            var clients = eventClients.Select(ec => ec.Client).ToList();

            var response = new GetEventClentsResponse();

            if (eventClients == null || !eventClients.Any())
            {
                response.Success = false;
                response.Message = "eventClients Not Found.";
                return response;
            }

            response.Success = true;
            response.Data = new List<ListClientDTO>();

            foreach (var eventClient in eventClients)
            {
                var clientDto = _mapper.Map<ListClientDTO>(eventClient.Client);
                //the below code will be replaced when ProfilePic coloumn added on the client table
                clientDto.ProfilePictureUrl = "";
                response.Data.Add(clientDto);
            }

            return response;

        }
    }
}
