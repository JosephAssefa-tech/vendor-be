using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.AddOn.DTOs
{
    public class CreateAddOnDTO : BaseDTO
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
        public Guid AddOnCategoryId { get; set; }
        public Guid RateStructureId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
