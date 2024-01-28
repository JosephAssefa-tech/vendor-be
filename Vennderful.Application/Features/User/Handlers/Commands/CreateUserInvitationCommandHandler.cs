using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using MediatR;
using Vennderful.Application.Features.User.Validators;
using System.Linq;
using Vennderful.Application.Features.User.DTOs;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Application.Contracts.Infrastructure.Mail;
using Vennderful.Application.Models.Mail;

namespace Vennderful.Application.Features.User.Handlers.Commands
{
    public class CreateUserInvitationCommandHandler : IRequestHandler<CreateUserInvitationCommand, CreateUserInvitationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        static Random random = new Random();

        public CreateUserInvitationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<CreateUserInvitationResponse> Handle(CreateUserInvitationCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new CreateUserInvitationDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateUserInvitationDTO);

            var response = new CreateUserInvitationResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var existingUser = await _unitOfWork.UserProfileRepository.GetUserProfileByEmail(request.CreateUserInvitationDTO.Email);
            if (existingUser == null)
            {
                try
                {
                    var invitation = _mapper.Map<UserProfile>(request.CreateUserInvitationDTO);
                    invitation = await _unitOfWork.UserProfileRepository.AddAsync(invitation);

                    if (invitation  != null)
                    {
                        var member = new Vennderful.Domain.Entities.Member
                        {
                            Email = invitation.Email,
                            IsActive = true,
                            UserID = random.Next(0, 999999).ToString("D6"),
                            JobTitle= request.CreateUserInvitationDTO.JobTitle,
                            UserRole = invitation.UserRole,
                            ProfileId = invitation.Id,
                            Profile=invitation,
                        };
                        member = await _unitOfWork.memberRepository.AddAsync(member);

                        if (member != null && !string.IsNullOrEmpty(request.CreateUserInvitationDTO.EventId))
                        {
                            var eventAndMember = new Vennderful.Domain.Entities.EventAndMember
                            {
                                EventId = Guid.Parse(request.CreateUserInvitationDTO.EventId),
                                MemberId = member.Id,
                                Member=member,
                                IsActive = true,
                            };
                            eventAndMember = await _unitOfWork.eventAndMemberRepository.AddAsync(eventAndMember);
                        }
                    }


                    await _unitOfWork.Save();

                    SendEmail(request.CreateUserInvitationDTO.Email, request.CreateUserInvitationDTO.CompanyId.ToString(),
                            request.CreateUserInvitationDTO.UserRole, EmailTemplates.Invitation);

                    response.Success = true;
                    response.Message = "Created Successfully.";
                    response.Data = _mapper.Map<CreateUserInvitationDTO>(invitation);

                    return response;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Something went wrong.";
                    response.Errors = new List<string>() { ex.Message };

                    return response;
                }
            }

            response.Success = false;
            response.Message = "A User with a similar email address already exist.";
            response.Errors = new List<string>() { "A User with a similar email address already exist" };

            return response;

        }
        private async void SendEmail(string to, string company, string role, EmailTemplates emailTemplate)
        {
            bool isEmailSent = false;
            try
            {
                await _emailService.SendEmail(to, company.ToString(),
                            role, emailTemplate);
                isEmailSent = true;
            }
            catch (Exception ex)
            {
                while (!isEmailSent)
                {
                    await _emailService.SendEmail(to, company.ToString(),
                            role, emailTemplate);
                    isEmailSent = true;
                }
            }
        }
    }
}
