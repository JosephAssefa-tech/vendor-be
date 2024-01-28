using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class AddOn : BaseAuditableEntity
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
        public Guid CompanyId { get; set; }

        [Display(Name = "AddOnCategory")]
        public virtual Guid AddOnCategoryId { get; set; }
        [Display(Name = "RateStructure")]
        public virtual Guid RateStructureId { get; set; }
        [ForeignKey("AddOnCategoryId")]
        public virtual AddOnCategory AddOnCategory { get; set; }
        [ForeignKey("RateStructureId")]
        public virtual RateStructure RateStructure { get; set; }

    }
}
