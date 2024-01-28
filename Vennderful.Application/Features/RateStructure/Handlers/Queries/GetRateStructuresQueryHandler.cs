using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.RateStructure.Requests;
using Vennderful.Application.Features.RateStructure.Responses;
using Vennderful.Application.Features.User.Handlers.Queries;
using Vennderful.Application.Features.RateStructure.DTOs;
using System.Collections.Generic;

namespace Vennderful.Application.Features.RateStructure.Handlers.Queries
{
    public class GetRateStructuresQueryHandler : IRequestHandler<GetRateStructuresRequest, GetRateStructuresResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRateStructuresQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetRateStructuresResponse> Handle(GetRateStructuresRequest request, CancellationToken cancellationToken)
        {
            var rate = await _unitOfWork.RateStructureRepository.GetAllAsync();
            var response = new GetRateStructuresResponse();
            if (rate == null)
            {
                response.Success = false;
                response.Message = "Rate Structure Not Found.";
                return response;
            }
            response.Success = true;
            response.Data = _mapper.Map<List<GetRateStructuresDTO>>(rate);
            return response;
        }

    }
}
