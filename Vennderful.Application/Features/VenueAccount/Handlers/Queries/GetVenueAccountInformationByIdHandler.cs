using Vennderful.Application.Models;
using Vennderful.Application.Contracts.Persitence;
using MediatR;
using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Features.VenueAccount.Requests;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Vennderful.Application.Features.VenueAccount.Responses;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.VenueAccount.Handlers.Queries
{
    public class GetVenueAccountInformationByIdHandler : IRequestHandler<GetVenueAccountInformationByIdQuery, GetVenueAccountInformationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetVenueAccountInformationByIdHandler> _logger;

        public GetVenueAccountInformationByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetVenueAccountInformationByIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<GetVenueAccountInformationResponse> Handle(GetVenueAccountInformationByIdQuery request, CancellationToken cancellationToken)
        {
            var venueAccountInformation = await _unitOfWork.VenueAccountInformationRepository.GetById(request.CompanyId);
            var response = new GetVenueAccountInformationResponse();
            if (venueAccountInformation == null)
            {
                response.Success = false;
                response.Message = "user Not Found.";
                return response;
            }
            response.Success = true;
            response.Data = _mapper.Map<VenueAccountInformation>(venueAccountInformation);
            return response;

        }

    }
}
