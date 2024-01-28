using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventFinance.Dto;

namespace Vennderful.Application.Features.EventFinance.Responses
{
    public class GetEventBudgetSummaryItemResponse : BaseResponse
    {
        public EventBudgetSummaryItemDto Data { get; set; }
    }
}
