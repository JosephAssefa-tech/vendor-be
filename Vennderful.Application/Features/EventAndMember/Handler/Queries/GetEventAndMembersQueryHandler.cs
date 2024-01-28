using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.Member.DTO;
using Vennderful.Application.Features.Member.Requests;
using Vennderful.Application.Features.Member.Responses;
using Vennderful.Application.Features.EventAndMember.Requests;
using Vennderful.Application.Features.EventAndMember.Responses;
using MediatR;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.EventAndMember.DTO;
using System.Linq;
using Vennderful.Domain.Enums;
using Vennderful.Domain.Entities;
using Microsoft.Extensions.Logging;
using Vennderful.Application.Models.Mail;

namespace Vennderful.Application.Features.EventAndMember.Handler.Queries
{
    public class GetEventAndMembersQueryHandler : IRequestHandler<GetEventAndMembersRequest, GetEventAndMembersResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventAndMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetEventAndMembersResponse> Handle(GetEventAndMembersRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetEventAndMembersResponse();
            try
            {
                var members = (await _unitOfWork.eventAndMemberRepository.GetMembersByEventId(request.EventId)).ToList();
                var eventMembers = members.Select(em => new ListEventAndMembersDTO
                {
                    EventId = em.EventId,
                    MemberId = em.MemberId,
                    IsActive = em.IsActive,
                    Status = em.IsActive ? "Pending" : "Confirmed",
                    UserId = em.Member.UserID,
                    Email = em.Member.Email,
                    FirstName = em.Member.FirstName,
                    LastName = em.Member.LastName,
                    Gender = em.Member.Gender.ToString(),
                    JobTitle = em.Member.JobTitle,
                    ProfilePicture = em.Member.ProfilePicture,
                    UserRole = em.Member.UserRole,
                }).ToList();
                response.Success = true;
                response.Data = eventMembers;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong.";
                response.Data = new List<ListEventAndMembersDTO>();
                response.Errors = new List<string>() { ex.Message };

                return response;
            }
        }
    }
}
