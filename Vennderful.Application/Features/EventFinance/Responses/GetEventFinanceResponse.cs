using Vennderful.Application.Common;
using Vennderful.Application.Features.EventFinance.Dto;

namespace Vennderful.Application.Features.EventFinance.Responses
{
    public class GetEventFinanceResponse : BaseResponse
    {
        public EventFinanceDto Data { get; set; }
    }
}
