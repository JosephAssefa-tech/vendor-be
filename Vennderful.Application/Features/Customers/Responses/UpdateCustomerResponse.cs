using Vennderful.Application.Common;
using Vennderful.Application.Features.Customers.DTOs;

namespace Vennderful.Application.Features.Customers.Responses
{
    public class UpdateCustomerResponse : BaseResponse
    {
        public UpdateCustomerDTO Data { get; set; }
    }
}
