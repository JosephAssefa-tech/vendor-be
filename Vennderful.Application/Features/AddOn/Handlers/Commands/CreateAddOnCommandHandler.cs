using AutoMapper;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.AddOn.DTOs;
using Vennderful.Application.Features.AddOn.Requests;
using Vennderful.Application.Features.AddOn.Responses;
using Vennderful.Application.Features.AddOn.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.AddOn.Handlers.Commands
{
    public class CreateAddOnCommandHandler : IRequestHandler<CreateAddOnCommand, CreateAddOnResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAddOnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateAddOnResponse> Handle(CreateAddOnCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new CreateAddOnDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateAddOnDTO);

            var response = new CreateAddOnResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }
            var existingAddon = await _unitOfWork.AddOnRepository.GetAddOnsByName(request.CreateAddOnDTO.AddOnName);
            if (existingAddon != null)
            {
                response.Success = false;
                response.Message = "Package with similar name already exists.";
                response.Errors = new List<string>() { "Package with similar name already exists." };

                return response;
            }
            var addOn = _mapper.Map<Domain.Entities.AddOn>(request.CreateAddOnDTO);
            addOn = await _unitOfWork.AddOnRepository.AddAsync(addOn);

            await _unitOfWork.Save();

            addOn = await _unitOfWork.AddOnRepository.GetAddOnsByName(request.CreateAddOnDTO.AddOnName);

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateAddOnResponseDTO>(addOn);

            return response;
        }
    }
}
