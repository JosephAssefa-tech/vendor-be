using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;

namespace Vennderful.Application.Features.NewDocuments.Handlers.Commands
{
    public class DeleteNewDocumentHandler : IRequestHandler<DeleteNewDocumentCommand, DeleteNewDocumentResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteNewDocumentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DeleteNewDocumentResponse> Handle(DeleteNewDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _unitOfWork.NewDocumentRepository.GetById(request.Id, request.companyId);

            var response = new DeleteNewDocumentResponse();

            if (document == null)
            {
                response.Success = false;
                response.Message = "Deletion Failed.";
                response.Errors = new List<string> { "Document Not Found." };
                return response;
            }

            await _unitOfWork.NewDocumentRepository.DeleteAsync(document);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Deleted Successfully.";

            return response;
        }
    }
}
