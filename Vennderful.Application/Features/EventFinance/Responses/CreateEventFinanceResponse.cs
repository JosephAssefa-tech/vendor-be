using Vennderful.Application.Common;
using Vennderful.Application.Features.EventFinance.Dto;

namespace Vennderful.Application.Features.EventFinance.Responses
{
    public class CreateEventFinanceResponse : BaseResponse
    {
        public CreateEventFinanceDto Data { get; set; }
    }
}
