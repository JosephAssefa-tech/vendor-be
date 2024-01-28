﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vennderful.Application.Features.User.DTOs
{
    public class UserRegisterDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
