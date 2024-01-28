using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;

namespace Vennderful.Application.Features.Client.Handlers.Queries
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsRequest, GetClientsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClientsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetClientsResponse> Handle(GetClientsRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetClientsResponse();
            try
            {
                var clients = (await _unitOfWork.clientRepository.GetClientsByName(request.SearchQuery, request.CompanyId)).ToList();

                response.Success = true;
                response.Data = _mapper.Map<List<ClientDTO>>(clients);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong.";
                response.Data = new List<ClientDTO>();
                response.Errors = new List<string>() { ex.Message };

                return response;
            }
        }
    }
}
