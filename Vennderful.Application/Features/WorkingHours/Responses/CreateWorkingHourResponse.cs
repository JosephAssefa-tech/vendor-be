using Vennderful.Application.Common;
using Vennderful.Application.Features.WorkingHours.DTOs;

namespace Vennderful.Application.Features.WorkingHours.Responses
{
    public class CreateWorkingHourResponse : BaseResponse
    {
        public CreateWorkingHourDto Data { get; set; }
    }
}
