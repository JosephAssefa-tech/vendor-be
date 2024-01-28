using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.PackageCategories.DTOs;
using Vennderful.Application.Features.PackageCategories.Requests;
using Vennderful.Application.Features.PackageCategories.Responses;

namespace Vennderful.Application.Features.PackageCategories.Handlers.Queries
{
    public class GetPackageCategoriesRequestHandler : IRequestHandler<GetPackageCategoriesRequest, GetPackageCategoriesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetPackageCategoriesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetPackageCategoriesResponse> Handle(GetPackageCategoriesRequest request, CancellationToken cancellationToken)
        {
            var packageCategories = await _unitOfWork.PackageCategoryRepository.GetAllAsync();
            var response = new GetPackageCategoriesResponse();
            if (packageCategories == null)
            {
                response.Success = false;
                response.Message = "Package Categories Not Found.";
                return response;

            }
            response.Success = true;
            response.Data = _mapper.Map<List<PackageCategoryDTo>>(packageCategories);
            return response;

        }
    }
}