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
using Vennderful.Application.Features.UserRoles.DTOs;
using Vennderful.Application.Features.UserRoles.Requests;
using Vennderful.Application.Features.UserRoles.Responses;
using Vennderful.Application.Features.UserRoles.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.UserRoles.Handlers.Commands
{
    public class AddUserRoleCommandHandler : IRequestHandler<AddUserRoleCommand, AddUserRoleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserRoleCommandHandler> _logger;
        public AddUserRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddUserRoleCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<AddUserRoleResponse> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddUserRoleDTOValidator();
            var validationResult = await validator.ValidateAsync(request.AddUserRoleDTO);

            var response = new AddUserRoleResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Adding User Role Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var userRole = _mapper.Map<UserRole>(request.AddUserRoleDTO);
            userRole = await _unitOfWork.UserRoleRepository.AddAsync(userRole);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Added Successfully.";
            response.Data = _mapper.Map<AddUserRoleDTO>(userRole);
            _logger.LogInformation($"UserRole {userRole.Id} is successfully added.");
            return response;
        }
    }
}
