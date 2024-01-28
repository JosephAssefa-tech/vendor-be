using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.EventFinance.Dto
{
    public class ListEventPaymentScheduleDto
    {
        public string _PaymentTitle = "";
        public int Id { get; set; }
        public string PaymentTitle { get { return _PaymentTitle; } set { _PaymentTitle = value; } }
        public Guid EventFinanceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal ScheduleAmount { get; set; }
        public string Status { get; set; }
        public ListEventPaymentScheduleDto()
        {
            _PaymentTitle = $"Payment #({Id})";
        }

    }
}
