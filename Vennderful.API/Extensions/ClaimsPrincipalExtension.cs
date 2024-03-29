﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Vennderful.API.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {

            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {

            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }


        public static int GetUserId(this ClaimsPrincipal user)
        {

            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        }     
    }
}
