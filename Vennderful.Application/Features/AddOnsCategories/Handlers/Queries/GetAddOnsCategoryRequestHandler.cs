using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.AddOnsCategories.DTOs;
using Vennderful.Application.Features.AddOnsCategories.Requests;
using Vennderful.Application.Features.AddOnsCategories.Responses;

namespace Vennderful.Application.Features.AddOnsCategories.Handlers.Queries
{

    public class GetAddOnsCategoryRequestHandler : IRequestHandler<GetAddOnsCategoryRequest, GetAddOnsCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAddOnsCategoryRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetAddOnsCategoryResponse> Handle(GetAddOnsCategoryRequest request, CancellationToken cancellationToken)
        {
            var addOnsCategory = await _unitOfWork.AddOnsCategoryRepository.GetAllAsync();
            var response = new GetAddOnsCategoryResponse();
            if (addOnsCategory == null)
            {
                response.Success = false;
                response.Message = "AddOnsCategory Not Found.";
                return response;

            }
            response.Success = true;
            response.Data = _mapper.Map<List<AddOnCategoryDTo>>(addOnsCategory);
            return response;

        }

    }
}