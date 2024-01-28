using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.User.DTOs;

namespace Vennderful.Application.Features.User.Responses
{
    public class GetCurrentUserResponse : BaseResponse
    {
        public UserProfileDTO Data { get; set; }
    }
}
 
