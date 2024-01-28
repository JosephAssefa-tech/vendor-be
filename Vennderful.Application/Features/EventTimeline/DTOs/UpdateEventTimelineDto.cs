using System;
using System.Collections.Generic;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.EventTimeline.DTOs
{
    public class UpdateEventTimelineDto
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string SlotTitle { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public List<ResponsiblePerson> ResponsiblePersons { get; set; }
    }
}
