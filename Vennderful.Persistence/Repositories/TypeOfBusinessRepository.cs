using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class TypeOfBusinessRepository : BaseRepository<TypeOfBusiness>, ITypeOfBusinessRepository
    {
        public TypeOfBusinessRepository(VennderfulDbContext dbContext) : base(dbContext)
        {
        }
    }
}
