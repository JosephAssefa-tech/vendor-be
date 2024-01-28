using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Features.UploadDocuments.DTOs;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface  IUploadDocumentRepository:IAsyncRepository<UploadDocument>
    {
        
    }
}
