using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.Client.DTOs;
using System.Linq;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Domain.Entities;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;

namespace Vennderful.Application.Features.Client.Handlers.Queries
{
    public class GetClientInvitesQueryHandler : IRequestHandler<GetClientInvitesRequest, GetClientInvitesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClientInvitesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetClientInvitesResponse> Handle(GetClientInvitesRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetClientInvitesResponse();
            try
            {
                var clients = (await _unitOfWork.clientRepository.GetInvitedClients(request.CompanyId));
                List<GetClientInvitesDTO> Invites = new List<GetClientInvitesDTO>();
                if (clients != null)
                {
                    Invites = clients.SelectMany(x => new List<GetClientInvitesDTO>()
                {
                    new GetClientInvitesDTO()
                    {
                        Email = x.Email,
                        //AccountType =x.AccountType,
                        Status=x.IsActive ? "Accepted" : "Pending acceptance",
                        Date= x.Created.ToString(),
                    },
                }).ToList();
                }

                response.Success = true;
                response.Data = _mapper.Map<List<GetClientInvitesDTO>>(Invites);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong.";
                response.Data = new List<GetClientInvitesDTO>();
                response.Errors = new List<string>() { ex.Message };

                return response;
            }
        }
    }
}
