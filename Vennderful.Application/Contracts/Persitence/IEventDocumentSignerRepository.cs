using System.Threading.Tasks;
using System;
using Vennderful.Domain.Entities;
using System.Collections.Generic;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventDocumentSignerRepository : IAsyncRepository<EventDocumentSigner>
    {
        Task<EventDocumentSigner> GetEventDocumentSignerByEventDocumentIdAndSignerId(Guid eventDocumentId, Guid signerId);
        Task<IReadOnlyList<EventDocumentSigner>> GetAllEventAddedDocumentsStatus(Guid eventDocumentId);
      
    }
}
