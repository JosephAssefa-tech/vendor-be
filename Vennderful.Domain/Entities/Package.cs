using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class Package: BaseAuditableEntity
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

        public Guid CompanyId { get; set; }

        [Display(Name = "PackageCategory")]
        public virtual Guid PackageCategoryId { get; set; }
        [Display(Name = "RateStructure")]
        public virtual Guid RateStructureId { get; set; }
        [ForeignKey("PackageCategoryId")]
        public virtual PackageCategory PackageCategory { get; set; }
        [ForeignKey("RateStructureId")]
        public virtual RateStructure RateStructure { get; set; }

    }
}
