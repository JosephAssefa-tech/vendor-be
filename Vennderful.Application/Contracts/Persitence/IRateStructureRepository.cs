using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IRateStructureRepository : IAsyncRepository<RateStructure>
    {
    }
}
