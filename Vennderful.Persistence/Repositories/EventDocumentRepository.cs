using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventDocumentRepository : BaseRepository<EventDocument>, IEventDocumentRepository
    {
        private readonly VennderfulDbContext _dbContext;
        public EventDocumentRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;

        }

        public async Task<IReadOnlyList<EventDocument>> GetAllEventAddedDocuments(Guid eventId)
        {
            var eventDocument = await _dbContext.EventDocuments
                  .Include(e => e.Document).Where(e => e.EventId == eventId)
                  .ToListAsync();

            return eventDocument;
        }
    }
}
