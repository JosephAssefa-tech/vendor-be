using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;
using Vennderful.Application.Features.Member.Responses;
using MediatR;
using Vennderful.Application.Features.Member.Requests;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.Client.DTOs;
using System.Linq;
using Vennderful.Application.Features.Member.DTO;

namespace Vennderful.Application.Features.Member.Handler.Queries
{
    public class GetMembersQueryHandler : IRequestHandler<GetMembersRequest, GetMembersResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetMembersResponse> Handle(GetMembersRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetMembersResponse();
            try
            {
                var members = (await _unitOfWork.memberRepository.GetMembersByCompanyId(request.CompanyId)).ToList();

                response.Success = true;
                response.Data = _mapper.Map<List<ListMembersDTO>>(members);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong.";
                response.Data = new List<ListMembersDTO>();
                response.Errors = new List<string>() { ex.Message };

                return response;
            }
        }
    }
}
