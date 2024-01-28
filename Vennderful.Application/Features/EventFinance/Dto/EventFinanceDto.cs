using System;
using System.Collections.Generic;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.EventFinance.Dto
{
    public class EventFinanceDto : BaseDTO
    {
        public Guid EventId { get; set; }
        public Guid PackageId { get; set; }
        public decimal PackagePrice { get; set; }
        public List<AddonDto> Addons { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime DepositDueDate { get; set; }
        public decimal TravelFees { get; set; }
        public List<PaymentSchedules> Payments { get; set; }
    }
    public class AddonDto
    {
        public Guid AddOnId { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
