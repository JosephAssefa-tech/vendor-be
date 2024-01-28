using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.TypeOfBusiness.DTOs;
using Vennderful.Application.Features.TypeOfBusiness.Requests;
using Vennderful.Application.Features.TypeOfBusiness.Responses;

namespace Vennderful.Application.Features.TypeOfBusiness.Handlers.Queries
{
    public class GetTypeOfBusinessRequestHandler : IRequestHandler<GetTypeOfBusinessRequest, GetTypeOfBusinessResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTypeOfBusinessRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetTypeOfBusinessResponse> Handle(GetTypeOfBusinessRequest request, CancellationToken cancellationToken)
        {
            var typeOfBusiness = await _unitOfWork.TypeOfBusinessRepository.GetByIdAsync(request.Id);
            var response = new GetTypeOfBusinessResponse();
            if(typeOfBusiness == null)
            {
                response.Success = false;
                response.Message = "TypeOfBusiness Not Found.";
                return response;

            }
            response.Success = true;
            response.Data = _mapper.Map<TypeOfBusinessDto>(typeOfBusiness);
            return response;



        }
    }
}