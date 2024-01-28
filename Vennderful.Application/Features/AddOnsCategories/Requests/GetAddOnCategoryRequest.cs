using MediatR;
using System;
using Vennderful.Application.Features.AddOnsCategories.Responses;

namespace Vennderful.Application.Features.AddOnsCategories.Requests
{
    public class GetAddOnCategoryRequest : IRequest<GetAddOnCategoryResponse>
    {
        public Guid Id { get; set; }
    }
}
