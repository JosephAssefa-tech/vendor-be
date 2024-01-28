using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.Customers.Validators;
using Vennderful.Domain.Entities;
using Vennderful.Application.Features.Customers.DTOs;

namespace Vennderful.Application.Features.Customers.Handlers.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new UpdateCustomerDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateCustomerDTO);

            var response = new UpdateCustomerResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var customer = _mapper.Map<Customer>(request.UpdateCustomerDTO);
            if(customer == null)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = new List<string> { "Customer Not Found."};
                return response;
            }

            await _unitOfWork.CustomerRepository.UpdateAsync(customer);

            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = new List<string> { "Bad Request." };

                return response;
            }

            response.Success = true;
            response.Message = "Updated Successfully.";
            response.Data = _mapper.Map<UpdateCustomerDTO>(customer);

            return response;
        }
    }
}
