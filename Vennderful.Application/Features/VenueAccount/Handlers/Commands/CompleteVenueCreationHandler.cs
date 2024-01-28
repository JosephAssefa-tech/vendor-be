using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Domain.Entities;
using Vennderful.Application.Features.VenueAccount.Requests;
using Vennderful.Application.Features.VenueAccount.Responses;
using MediatR;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.VenueAccount.Validators;
using System.Linq;
using Vennderful.Application.Features.VenueAccount.DTOs;

namespace Vennderful.Application.Features.VenueAccount.Handlers.Commands
{
    public class CompleteVenueCreationHandler : IRequestHandler<CompleteVenueCreationCommand, CompleteVenueCreationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompleteVenueCreationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CompleteVenueCreationResponse> Handle(CompleteVenueCreationCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new CompleteVenueCreationDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CompleteVenueCreationDTO);

            var response = new CompleteVenueCreationResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Update failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var existingVenue = await _unitOfWork.VenueAccountInformationRepository.GetVenueByCompanyName(request.CompleteVenueCreationDTO.CompanyName, request.CompleteVenueCreationDTO.CompanyId);
           
            if (existingVenue != null)
            {
                if (existingVenue.PhoneNumber != null)  //should do a check for other mandatory information like payment, package, etc
                {
                    existingVenue.Status = request.CompleteVenueCreationDTO.Status;
                    await _unitOfWork.VenueAccountInformationRepository.UpdateAsync(existingVenue);
                    try
                    {
                        await _unitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        response.Message = "Update Failed.";
                        response.Errors = new List<string> { "Bad Request." };

                        return response;
                    }

                    response.Success = true;
                    response.Message = "Updated Successfully.";
                    response.Data = _mapper.Map<CompleteVenueCreationDTO>(existingVenue);

                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update Failed.";
                    response.Errors = new List<string> { "You must fill out all requiered data before publishing your profile." };
                    return response;
                }
            }
            response.Success = false;
            response.Message = "Update Failed.";
            response.Errors = new List<string> { "Venue Not Found." };
            return response;
        }
    }
}
