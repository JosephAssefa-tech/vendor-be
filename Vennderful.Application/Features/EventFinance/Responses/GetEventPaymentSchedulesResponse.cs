using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventFinance.Dto;

namespace Vennderful.Application.Features.EventFinance.Responses
{
    public class GetEventPaymentSchedulesResponse : BaseResponse
    {
        public List<ListEventPaymentScheduleDto> Data { get; set; }
    }
}
