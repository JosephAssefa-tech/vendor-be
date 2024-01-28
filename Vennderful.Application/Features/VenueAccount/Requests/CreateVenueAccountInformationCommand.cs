using MediatR;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Application.Features.VenueAccount.Responses;

namespace Vennderful.Application.Features.VenueAccount.Requests
{
    public class CreateVenueAccountInformationCommand : IRequest<CreateVenueAccountInformationResponse>
    {
        public CreateVenueAccountInformationDto CreateVenueAccountInformationDto { get;set;}

    }
}
