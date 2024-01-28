using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IDocumentRepository:IAsyncRepository<Document>
    {
        Task<IReadOnlyList<Document>> GetAllAddedDocuments(Guid companyId);
        Task<Document> GetById(Guid Id, Guid companyId);
        Task<Document> GetById(Guid Id);
        Task<IEnumerable<Document>> GetAllDocumentTemplates(Guid companyId);


    }
}
