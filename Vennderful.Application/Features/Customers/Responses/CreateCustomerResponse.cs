using Vennderful.Application.Common;
using Vennderful.Application.Features.Customers.DTOs;

namespace Vennderful.Application.Features.Customers.Responses
{
    public class CreateCustomerResponse : BaseResponse
    {
        public CreateCustomerDTO Data { get; set; }
    }
}
