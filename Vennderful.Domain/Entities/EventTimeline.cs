using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class EventTimeline : BaseAuditableEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public string SlotTitle { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string ResponsiblePersonsJson { get; set; }
        public List<ResponsiblePerson> ResponsiblePersons
        {
            get => JsonConvert.DeserializeObject<List<ResponsiblePerson>>(ResponsiblePersonsJson);
            set => ResponsiblePersonsJson = JsonConvert.SerializeObject(value);
        }
    }

    [NotMapped]
    public class ResponsiblePerson
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}
