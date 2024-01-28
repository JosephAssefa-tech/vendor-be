using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class EventDocumentSignerRepository : BaseRepository<EventDocumentSigner>, IEventDocumentSignerRepository
    {
        private readonly VennderfulDbContext _dbContext;
        public EventDocumentSignerRepository(VennderfulDbContext context) : base(context)
        {
            _dbContext = context;

        }

        public async Task<IReadOnlyList<EventDocumentSigner>> GetAllEventAddedDocumentsStatus(Guid eventDocumentId)
        {
           

            var eventDocumentStatus = await _dbContext.EventDocumentSigners
                .Where(x => x.EventDocumentId != Guid.Empty && x.EventDocumentId == eventDocumentId)
                .ToListAsync();

            return eventDocumentStatus;
        }



        public async Task<EventDocumentSigner> GetEventDocumentSignerByEventDocumentIdAndSignerId(Guid eventDocumentId, Guid signerId)
        {
            var eventDocumentSigner = (await GetQueryAsync(x => x.EventDocumentId != Guid.Empty && x.EventDocumentId == eventDocumentId 
                                                                 && x.SignerId != Guid.Empty && x.SignerId == signerId)).FirstOrDefault();
            return eventDocumentSigner;
        }
    }
}
