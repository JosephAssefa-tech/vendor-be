using MediatR;
using Vennderful.Application.Features.WorkingHours.DTOs;
using Vennderful.Application.Features.WorkingHours.Responses;

namespace Vennderful.Application.Features.WorkingHours.Requests
{
    public class CreateWorkingHourCommand : IRequest<CreateWorkingHourResponse>
    {
        public CreateWorkingHourDto CreateWorkingHourDto { get;set;}

    }
}
