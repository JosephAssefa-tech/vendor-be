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
    public class EventAndRoomRepository : BaseRepository<EventAndRoom>, IEventAndRoomRepository
    {
        private readonly VennderfulDbContext _dbContext;
        public EventAndRoomRepository(VennderfulDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> CheckEventAndRoomExists(Guid companyId, string eventName)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<EventAndRoom>> GetEventAndRooms(Guid companyId)
        {
            return await(await GetQueryAsync(x => x.CompanyId == companyId)).ToListAsync();
        }

        public async Task<IReadOnlyList<EventAndRoom>> GetEventAndRoomsByEventId(Guid eventId)
        {
            return await (await GetQueryAsync(x => x.EventId == eventId)).ToListAsync();
        }
    }
}  
