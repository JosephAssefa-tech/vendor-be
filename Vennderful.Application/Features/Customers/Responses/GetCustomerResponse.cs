
using Vennderful.Application.Common;
using Vennderful.Application.Features.Customers.DTOs;

namespace Vennderful.Application.Features.Customers.Responses
{
    public class GetCustomerResponse : BaseResponse
    {
        public CustomerDTO Data { get; set; }
    }
}
