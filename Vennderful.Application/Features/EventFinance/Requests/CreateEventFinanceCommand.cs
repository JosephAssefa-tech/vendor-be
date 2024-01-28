using MediatR;
using Vennderful.Application.Features.EventFinance.Dto;
using Vennderful.Application.Features.EventFinance.Responses;

namespace Vennderful.Application.Features.EventFinance.Requests
{
    public class CreateEventFinanceCommand : IRequest<CreateEventFinanceResponse>
    {
        public CreateEventFinanceDto CreateEventFinanceDto { get; set; }
    }
}
