using AutoMapper;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Package.DTOs;
using Vennderful.Application.Features.Package.Requests;
using Vennderful.Application.Features.Package.Responses;
using Vennderful.Application.Features.Package.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.Package.Handlers.Commands
{
    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, CreatePackageResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePackageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreatePackageResponse> Handle(CreatePackageCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new CreatePackageDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreatePackageDTO);

            var response = new CreatePackageResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var existingPackage = await _unitOfWork.PackageRepository.GetPackageByName(request.CreatePackageDTO.PackageName);
            if (existingPackage != null)
            {
                response.Success = false;
                response.Message = "Package with similar name already exists.";
                response.Errors = new List<string>() { "Package with similar name already exists." };

                return response;
            }

            var package = _mapper.Map<Domain.Entities.Package>(request.CreatePackageDTO);
            package = await _unitOfWork.PackageRepository.AddAsync(package);

            await _unitOfWork.Save();

            package = await _unitOfWork.PackageRepository.GetPackageByName(request.CreatePackageDTO.PackageName);

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreatePackageResponseDTO>(package);

            return response;
        }
    }
}
