using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.UploadDocuments.DTOs;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class UploadDocumentRepository : BaseRepository<UploadDocument>, IUploadDocumentRepository
    {
        public UploadDocumentRepository(VennderfulDbContext dbContext) : base(dbContext)
        {
        }
    }
}
