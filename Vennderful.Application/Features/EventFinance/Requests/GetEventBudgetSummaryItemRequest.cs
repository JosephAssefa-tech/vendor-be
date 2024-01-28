using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventFinance.Responses;

namespace Vennderful.Application.Features.EventFinance.Requests
{
    public class GetEventBudgetSummaryItemRequest : IRequest<GetEventBudgetSummaryItemResponse>
    {
        public Guid EventId { get; set; }
        public Guid PackageId { get; set; }
        public Guid AddonId { get; set; }
        
    }
}
