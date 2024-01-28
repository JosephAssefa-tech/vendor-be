
using System;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.Client.DTOs
{
    public class ListClientDTO : BaseDTO
    {
        //public string UserRole { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Gender { get; set; }
        public string? AccountType { get; set; }
        public string? Phone { get; set; }
        public Address Address { get; set; }
        public string? Note { get; set; }
        //public string? Status { get; set; } 
       // public int EventId { get; set; }
       // public virtual ListEventDTO Event { get; set; }

    }
}
