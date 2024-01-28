using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.AddOn.DTOs;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.EventFinance.Dto
{
    public class EventBudgetSummaryItemDto
    {
        public Guid EventId { get; set; }
        public string PackageName { get; set; }
        public decimal PackagePrice { get; set; }
        public string Status { get; set; }
        public decimal TravelFees { get; set; }
        public decimal DepositAmount { get; set; }
        public string AddonName { get; set; }
        public decimal AddonTotalPrice { get; set; }

    }
}
