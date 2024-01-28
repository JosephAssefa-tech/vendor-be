using System;
using System.Collections.Generic;

namespace Vennderful.Application.Features.EventFinance.Dto
{
    public class CreateEventFinanceDto
    {
        public Guid EventId { get; set; }
        public Guid PackageId { get; set; }
        public decimal PackagePrice { get; set; }
        public List<Vennderful.Domain.Entities.Addon> Addons { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime DepositDueDate { get; set; }
        public decimal TravelFees { get; set; }
        public List<PaymentSchedules> Payments { get; set; }
    }
    public class PaymentSchedules
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal ScheduleAmount { get; set; }
    }
 }
