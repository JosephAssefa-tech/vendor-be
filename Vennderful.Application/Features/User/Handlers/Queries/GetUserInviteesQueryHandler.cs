using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.User.DTOs;
using System.Linq;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.User.Handlers.Queries
{
    public class GetUserInvitesQueryHandler : IRequestHandler<GetUserInvitesRequest, GetUserInvitesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserInvitesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserInvitesResponse> Handle(GetUserInvitesRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetUserInvitesResponse();
            try
            {
                var userProfiles = (await _unitOfWork.UserProfileRepository.GetInvitedUsers(request.CompanyId));
                List<GetUserInvitesDTO> Invites = new List<GetUserInvitesDTO>();
                if (userProfiles != null)
                {
                    Invites = userProfiles.SelectMany(x => new List<GetUserInvitesDTO>()
                {
                    new GetUserInvitesDTO()
                    {
                        Email = x.Email,
                        Role=x.UserRole,
                        Status=x.IsActive ? "Accepted" : "Pending acceptance",
                        Date= x.Created.ToString(),
                    },
                }).ToList();
                }

                response.Success = true;
                response.Data = _mapper.Map<List<GetUserInvitesDTO>>(Invites);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong.";
                response.Data = new List<GetUserInvitesDTO>();
                response.Errors = new List<string>() { ex.Message };

                return response;
            }
        }
    }
}
