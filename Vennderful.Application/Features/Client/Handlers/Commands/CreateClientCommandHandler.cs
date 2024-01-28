using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using MediatR;
using Vennderful.Application.Features.Client.Validators;
using System.Linq;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;
using Vennderful.Application.Contracts.Infrastructure.Mail;
using Vennderful.Application.Models.Mail;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.Client.Handlers.Commands
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CreateClientResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<CreateClientResponse> Handle(CreateClientCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new CreateClientInvitationDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateClientDTO);

            var response = new CreateClientResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var existingClient = await _unitOfWork.clientRepository.GetClientByEmail(request.CreateClientDTO.Email);
            if (existingClient == null || existingClient.Count() == 0)
            {
                try
                {
                    var client = _mapper.Map<Vennderful.Domain.Entities.Client>(request.CreateClientDTO);
                    client = await _unitOfWork.clientRepository.AddAsync(client);
                    await _unitOfWork.Save();

                    var eventClient = new EventClient()
                    {
                        EventId = request.EventId,
                        ClientId = client.Id,
                        Note = request.CreateClientDTO.Note,
                        Status = Domain.Enums.InvitationStatus.Pending,
                    };

                    eventClient = await _unitOfWork.eventClientRepository.AddAsync(eventClient);
                    await _unitOfWork.Save();

                    var evnt = await _unitOfWork.eventRepository.GetById(request.EventId);

                    _emailService.SendEmail(client.Email, client.LastName + " " + client.FirstName, evnt.EventName, evnt.Id, client.Id, EmailTemplates.EventInvitation);


                    response.Success = true;
                    response.Message = "Created Successfully.";
                    response.Data = _mapper.Map<CreateClientDTO>(client);
                    response.Data.Note = request.CreateClientDTO.Note;

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
            response.Message = "A Client with a similar email address already exist.";
            response.Errors = new List<string>() { "A Client with a similar email address already exist" };

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
                    SendEmail(to, company.ToString(), role, emailTemplate);
                }
            }
        }
    }
}
