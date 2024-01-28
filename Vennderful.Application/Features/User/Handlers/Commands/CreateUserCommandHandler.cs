using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Application.Features.User.Validators;
using AutoMapper;
using Vennderful.Application.Features.User.DTOs;

namespace Vennderful.Application.Features.User.Handlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserProfile>(request.UserRegisterDto);
            var validator = new RegisterUserDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UserRegisterDto);

            var response = new CreateUserResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "User Registration Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response; 
            }

            // check if the user signup based on an invitation
            var existingUser = await _unitOfWork.UserProfileRepository.GetUserProfileByEmail(request.UserRegisterDto.Email);
            if (existingUser == null)
            {
                user.CompanyId = Guid.NewGuid();
                user.InitialCreated = true;
                user.IsActive = true;
                user = await _unitOfWork.UserProfileRepository.AddAsync(user);
            }
            else
            {
                // if the user comes through invitation we just need to update some information
                existingUser.IsActive = true;
                existingUser.UserId = request.UserRegisterDto.UserId;
                existingUser.FirstName = request.UserRegisterDto.FirstName;
                existingUser.LastName = request.UserRegisterDto.LastName;
                existingUser.InitialCreated = false;
                await _unitOfWork.UserProfileRepository.UpdateAsync(existingUser);
                user = existingUser;
            }
                
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "User Registered Successfully.";
            _logger.LogInformation($"User {user.Id} is successfully created.");
            response.Data = _mapper.Map<UserProfileDTO>(user);

            return response;
        }
    }
}
