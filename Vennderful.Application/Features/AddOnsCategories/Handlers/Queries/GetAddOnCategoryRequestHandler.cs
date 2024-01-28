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
    public class GetAddOnCategoryRequestHandler : IRequestHandler<GetAddOnCategoryRequest, GetAddOnCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAddOnCategoryRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetAddOnCategoryResponse> Handle(GetAddOnCategoryRequest request, CancellationToken cancellationToken)
        {
            var addOnCategory = await _unitOfWork.AddOnsCategoryRepository.GetByIdAsync(request.Id);
            var response = new GetAddOnCategoryResponse();
            if (addOnCategory == null)
            {
                response.Success = false;
                response.Message = "AddOnCategory Not Found.";
                return response;

            }
            response.Success = true;
            response.Data = _mapper.Map<AddOnCategoryDTo>(addOnCategory);
            return response;



        }
    }
}