using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
  
    public interface IWorkingHoursRepository : IAsyncRepository<WorkingHour>
    {
    }
}
