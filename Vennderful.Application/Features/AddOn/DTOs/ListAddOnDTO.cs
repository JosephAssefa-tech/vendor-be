using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.AddOn.DTOs
{
    public class ListAddOnDTO : BaseDTO
    {
        public string AddOnName { get; set; }
        public decimal PricePerUnit { get; set; }
        public Boolean Taxable { get; set; }
        public string AddOnImageUrl { get; set; }
        public string AddOnDescription { get; set; }
        public string AddOnNote { get; set; }
        public string DefaultDeposit { get; set; }
        public string DefaultPrice { get; set; }
        public string Percent { get; set; }
        public string HourlyRate { get; set; }
        public string MinimumHours { get; set; }
        public string Duration { get; set; }
        public string DurationPrice { get; set; }
        public string DurationOverTime { get; set; }
        public string PerHeadPrice { get; set; }
        public string HeadDuration { get; set; }
        public AddOnCategory AddOnCategory { get; set; }
        public Domain.Entities.RateStructure RateStructure { get; set; }
    }
}
