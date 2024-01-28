﻿using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Domain.Entities
{
    public class Member : BaseAuditableEntity
    {
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public string JobTitle { get; set; }
        public Phone[]? Phone { get; set; }
        public Address? Address { get; set; }
        public string UserID { get; set; }
        public string? ProfilePicture { get; set; }
        public string UserRole { get; set; }
        public Guid ProfileId { get; set; }

        public UserProfile Profile { get; set; }
    }
}
