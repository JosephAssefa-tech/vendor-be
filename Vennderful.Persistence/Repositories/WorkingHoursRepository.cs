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
    public class WorkingHoursRepository : BaseRepository<WorkingHour>, IWorkingHoursRepository
    {
        public WorkingHoursRepository(VennderfulDbContext context) : base(context)
        {

        }
    }
}
