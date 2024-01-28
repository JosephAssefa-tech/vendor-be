
using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Customers.DTOs;

namespace Vennderful.Application.Features.Customers.Responses
{
    public class GetCustomersResponse : BaseResponse
    {
        public List<ListCustomerDTO> Data { get; set; }
    }
}
