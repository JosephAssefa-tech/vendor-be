using MediatR;
using Vennderful.Application.Features.EventAndMember.DTO;
using Vennderful.Application.Features.EventAndMember.Responses;
using Vennderful.Application.Features.Member.DTO;

namespace Vennderful.Application.Features.EventAndMember.Requests
{
    public class CreateEventAndMemberCommand : IRequest<CreateEventAndMemberResponse>
    {
        public CreateEventAndMemberDTO CreateEventAndMemberDTO {get; set; }
    }
}
