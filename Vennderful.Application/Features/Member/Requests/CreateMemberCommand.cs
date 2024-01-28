using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Member.DTO;
using Vennderful.Application.Features.Member.Responses;

namespace Vennderful.Application.Features.Member.Requests
{
    public class CreateMemberCommand : IRequest<CreateMemberResponse>
    {
        public CreateMemberDTO CreateMemberDTO { get; set; }
    }
}
