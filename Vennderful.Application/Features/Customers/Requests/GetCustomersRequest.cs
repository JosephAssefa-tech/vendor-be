using MediatR;
using Vennderful.Application.Features.Customers.Responses;

namespace Vennderful.Application.Features.Customers.Requests
{
    public class GetCustomersRequest : IRequest<GetCustomersResponse>
    {
    }
}
