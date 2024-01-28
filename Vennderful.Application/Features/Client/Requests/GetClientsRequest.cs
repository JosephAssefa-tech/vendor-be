using MediatR;
using Vennderful.Application.Features.Client.Responses;

namespace Vennderful.Application.Features.Client.Requests
{
    public class GetClientsRequest : IRequest<GetClientsResponse>
    {
        public string SearchQuery { get; set; }
        public string CompanyId { get; set; }
    }
}
