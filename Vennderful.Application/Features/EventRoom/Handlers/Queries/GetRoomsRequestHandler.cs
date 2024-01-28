using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventRoom.Dto;
using Vennderful.Application.Features.EventRoom.Requests;
using Vennderful.Application.Features.EventRoom.Responses;

namespace Vennderful.Application.Features.EventRoom.Handlers.Queries
{
    public class GetRoomsRequestHandler : IRequestHandler<GetRoomsRequest, GetRoomsResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoomsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetRoomsResponse> Handle(GetRoomsRequest request, CancellationToken cancellationToken)
        {
           var rooms =  (await _unitOfWork.RoomRepository.GetAllRooms(request.CompanyId)).OrderByDescending(c => c.Created);
            var response = new GetRoomsResponse();
            response.Success = true;
            response.Data = _mapper.Map<List<ListRoomDto>>(rooms);
            return response;
        }
    }
}
