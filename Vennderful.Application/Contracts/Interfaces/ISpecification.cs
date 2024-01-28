using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vennderful.Application.Contracts.Interfaces
{
    public interface ISpecification<TEntity>
    {
        TEntity SatisfyingEntityFrom(IQueryable<TEntity> query);

        IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query);
    }
}
