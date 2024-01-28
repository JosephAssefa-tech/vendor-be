using MediatR;
using System;
using Vennderful.Application.Features.PackageCategories.Responses;

namespace Vennderful.Application.Features.PackageCategories.Requests
{
    public class GetPackageCategoryRequest : IRequest<GetPackageCategoryResponse>
    {
        public Guid Id { get; set; }
    }
}
