using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.Package.DTOs
{
    public class CreatePackageDTO 
    {
        public string PackageName { get; set; }
        public Boolean Taxable { get; set; }
        public string PackageImageUrl { get; set; }
        public string PackageDescription { get; set; }
        public string PackageNote { get; set; }
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
        public Guid PackageCategoryId { get; set; }
        public Guid RateStructureId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
