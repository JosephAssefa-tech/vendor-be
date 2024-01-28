using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventAndMemberRepository : BaseRepository<EventAndMember>, IEventAndMemberRepository
    {
        public EventAndMemberRepository(VennderfulDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<EventAndMember>> GetMembersByEventId(Guid eventId)
        {
            return (await GetQueryAsync(x => x.EventId == eventId)).Include(x => x.Event).Include(x => x.Member).ToList();
        }
    }
}
