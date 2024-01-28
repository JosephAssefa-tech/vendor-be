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
    public class RateStructureRepository : BaseRepository<RateStructure>, IRateStructureRepository
    {
        public RateStructureRepository(VennderfulDbContext dbContext) : base(dbContext) { }
    }
}
