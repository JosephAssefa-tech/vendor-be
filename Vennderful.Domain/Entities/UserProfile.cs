using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vennderful.Domain.Common;
using Vennderful.Domain.ValueObjects;


namespace Vennderful.Domain.Entities
{
    public class UserProfile : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Address Address { get; set; }
        public bool InitialCreated { get; set; }
        public string? Status { get; set; }
        public string? UserRole { get; set; }
        public Guid CompanyId { get; set; }
    }
}
