using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.TypeOfBusiness.DTOs;
using Vennderful.Application.Features.TypeOfBusiness.Requests;
using Vennderful.Application.Features.TypeOfBusiness.Responses;

namespace Vennderful.Application.Features.TypeOfBusiness.Handlers.Queries
{
    public class GetTypesOfBusinessRequestHandler : IRequestHandler<GetTypesOfBusinessRequest, GetTypesOfBusinessResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTypesOfBusinessRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetTypesOfBusinessResponse> Handle(GetTypesOfBusinessRequest request, CancellationToken cancellationToken)
        {
            var typesOfBusiness = (await _unitOfWork.TypeOfBusinessRepository.GetAllAsync()).OrderBy(c => c.Created);
            var response = new GetTypesOfBusinessResponse();
            response.Success = true;
            response.Data = _mapper.Map<List<ListTypeOfBusinessDto>>(typesOfBusiness);
            return response;

        }
    }
}
