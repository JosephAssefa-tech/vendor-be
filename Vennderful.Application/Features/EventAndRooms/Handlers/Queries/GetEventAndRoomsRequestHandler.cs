using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventAndRooms.Dto;
using Vennderful.Application.Features.EventAndRooms.Requests;
using Vennderful.Application.Features.EventAndRooms.Responses;

namespace Vennderful.Application.Features.EventAndRooms.Handlers.Queries
{
    public class GetEventAndRoomsRequestHandler : IRequestHandler<GetEventAndRoomsRequest, GetEventAndRoomsResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEventAndRoomsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetEventAndRoomsResponse> Handle(GetEventAndRoomsRequest request, CancellationToken cancellationToken)
        {
            var result = (await _unitOfWork.EventAndRoomRepository.GetAllAsync()).OrderBy(c => c.Created);
            var response = new GetEventAndRoomsResponse();
            response.Success = true;
            response.Data = _mapper.Map<List<ListEventAndRoomDto>>(result);
            return response;
        }
    }
}
