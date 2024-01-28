using System;
using System.Collections.Generic;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IPackageCategoryRepository : IAsyncRepository<PackageCategory>
    {
    }
}

