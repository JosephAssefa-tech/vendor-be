using Microsoft.EntityFrameworkCore;
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
    public class EventAndClientRepository : BaseRepository<EventAndClient>, IEventAndClientRepository
    {
        private readonly VennderfulDbContext _dbContext;

        public EventAndClientRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<IReadOnlyList<EventAndClient>> GetAllEventsAsync(Guid companyId)
        {
            return await (await GetQueryAsync(x => x.CompanyId == companyId)).ToListAsync();
        }
    }
}
