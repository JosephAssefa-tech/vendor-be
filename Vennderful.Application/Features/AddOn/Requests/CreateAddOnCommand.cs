using MediatR;
using Vennderful.Application.Features.AddOn.DTOs;
using Vennderful.Application.Features.AddOn.Responses;

namespace Vennderful.Application.Features.AddOn.Requests
{
    public class CreateAddOnCommand : IRequest<CreateAddOnResponse>
    {
        public CreateAddOnDTO CreateAddOnDTO { get; set; }
    }
}
