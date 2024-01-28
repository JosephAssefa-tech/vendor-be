using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        private readonly VennderfulDbContext _dbContext;
        public DocumentRepository(VennderfulDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<Document>> GetAllAddedDocuments(Guid companyId)
        {
            return await (await GetQueryAsync(x => x.CompanyId == companyId)).ToListAsync();

        }
        public async Task<Document> GetById(Guid Id, Guid companyId)
        {
            var document = (await GetQueryAsync(x => x.Id == Id && x.CompanyId == companyId)).FirstOrDefault();
            return document;
        }

        public async Task<Document> GetById(Guid Id)
        {
            var document = (await GetQueryAsync(x => x.Id == Id)).FirstOrDefault();
            return document;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentTemplates(Guid companyId)
        {
            return await _dbContext.Documents.ToListAsync();

        }

    }
}
