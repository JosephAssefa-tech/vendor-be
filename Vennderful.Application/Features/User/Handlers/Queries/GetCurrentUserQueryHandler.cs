using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.User.DTOs;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.User.Handlers.Queries
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUser, GetCurrentUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCurrentUserQueryHandler> _logger;
        private readonly IUserProfileRepository _userProfileRepository;

        public GetCurrentUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetCurrentUserQueryHandler> logger, IUserProfileRepository userProfileRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userProfileRepository = userProfileRepository;
           
        }
        public async Task<GetCurrentUserResponse> Handle(GetCurrentUser request, CancellationToken cancellationToken)
        {
            var response = new GetCurrentUserResponse();
            try
            {
            var user = await _userProfileRepository.GetUserProfileByUserId(request.IdentityId);
                        
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "user Not Found.";
                    return response;
                }
                response.Success = true;
                response.Data = _mapper.Map<UserProfileDTO>(user);
                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
