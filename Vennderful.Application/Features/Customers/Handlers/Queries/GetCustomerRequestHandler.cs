using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;

namespace Vennderful.Application.Features.Customers.Handlers.Queries
{
    public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);
            var response = new GetCustomerResponse();
            if (customer == null)
            {
                response.Success = false;
                response.Message = "Customer Not Found.";
                return response;
            }
            response.Success = true;
            response.Data = _mapper.Map<CustomerDTO>(customer);
            return response;
        }
    }
}
