using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.UserRoles.DTOs;
using Vennderful.Application.Features.UserRoles.Requests;
using Vennderful.Application.Features.UserRoles.Responses;

namespace Vennderful.Application.Features.UserRoles.Handlers.Queries
{
    public class GetRoleByUserQueryHandler : IRequestHandler<GetRoleByUserQuery, GetUserRoleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleByUserQueryHandler> _logger;
        private readonly IUserRoleRepository _userRoleRepository;
        public GetRoleByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetRoleByUserQueryHandler> logger, IUserRoleRepository userRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRoleRepository = userRoleRepository;
        }
        public async Task<GetUserRoleResponse> Handle(GetRoleByUserQuery request, CancellationToken cancellationToken)
        {
            var userRole = await _userRoleRepository.GetByCompanyIdAsync(request.CompanyId);
            var response = new GetUserRoleResponse();
            if (userRole == null)
            {
                response.Success = false;
                response.Message = "user role Not Found.";
                return response;
            }
            response.Success = true;
            response.Data = _mapper.Map<UserRoleDTO>(userRole);
            return response;
        }
    }
}
