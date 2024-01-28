using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Domain.Common;
using Vennderful.Domain.Entities;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.Client.DTOs
{
    public class ClientDTO : BaseDTO
    {
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public AccountType AccountType { get; set; }
        public string? CompanyName { get; set; }
        public Phone[] Phone { get; set; }
        public Address Address { get; set; }
        public List<EventDTO>? Events { get; set; } 
    }
}
