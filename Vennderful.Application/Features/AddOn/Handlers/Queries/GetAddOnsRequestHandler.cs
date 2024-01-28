using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.AddOn.DTOs;
using Vennderful.Application.Features.AddOn.Requests;
using Vennderful.Application.Features.AddOn.Responses;

namespace Vennderful.Application.Features.AddOn.Handlers.Queries
{
    public class GetAddOnsRequestHandler : IRequestHandler<GetAddOnsRequest, GetAddOnsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAddOnsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetAddOnsResponse> Handle(GetAddOnsRequest request,
            CancellationToken cancellationToken)
        {
            var addOns = (await _unitOfWork.AddOnRepository.GetAllAddOnsWithCategoriesAsync(request.CompanyId)).OrderByDescending(c => c.Created).Take(5);
            var response = new GetAddOnsResponse();
            response.Success = true;
            response.Data = _mapper.Map<List<ListAddOnDTO>>(addOns);
            return response;
        }
    }
}
