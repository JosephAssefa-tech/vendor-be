using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventDocumentRepository : IAsyncRepository<EventDocument>
    {
        Task<IReadOnlyList<EventDocument>> GetAllEventAddedDocuments(Guid eventId);
    }
}
