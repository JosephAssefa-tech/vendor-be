using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Package.DTOs;
using Vennderful.Application.Features.Package.Requests;
using Vennderful.Application.Features.Package.Responses;

namespace Vennderful.Application.Features.Package.Handlers.Queries
{
    public class GetPackagesRequestHandler : IRequestHandler<GetPackagesRequest, GetPackagesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPackagesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetPackagesResponse> Handle(GetPackagesRequest request,
            CancellationToken cancellationToken)
        {
            var packages = (await _unitOfWork.PackageRepository.GetAllPackages(request.CompanyId)).OrderByDescending(c => c.Created).Take(10);
            var response = new GetPackagesResponse();
            response.Success = true;
            response.Data = _mapper.Map<List<ListPackageDTO>>(packages);
            return response;
        }
    }
}
