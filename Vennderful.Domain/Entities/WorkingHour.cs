using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class WorkingHour : BaseAuditableEntity
    {
        public string? MondayOpeningHour { get; set; } 
        public string? MondayClosingHour { get; set; }
        public string? TuesdayOpeningHour { get; set; }
        public string? TuesdayClosingHour { get; set; }
        public string? WednesdayOpeningHour { get; set; }
        public string? WednesdayClosingHour { get; set; }
        public string? ThursdayOpeningHour { get; set; }
        public string? ThursdayClosingHour { get; set; }
        public string? FridayOpeningHour { get; set; }
        public string? FridayClosingHour { get; set; }
        public string? SaturdayOpeningHour { get; set; }
        public string? SaturdayClosingHour { get; set; }
        public string? SundayOpeningHour { get; set; }
        public string? SundayClosingHour { get; set; }
    }
}


