using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Infrastructure.Mail;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.Customers.Validators;
using Vennderful.Application.Models.Mail;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.Customers.Handlers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, 
            CancellationToken cancellationToken)
        {
            var response = new CreateCustomerResponse();

            try
            {
                await _emailService.SendEmail(request.email);
                response.Success = true;
                response.Data = null;
                response.Message = "An Email Sent!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = $"Email failed to send.";
                response.Message = $"{ex.Message}";
            }

            return response;
        }
    }
}
