using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Domain.Entities;
using MediatR;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using System.Linq;
using Vennderful.Application.Features.Documents.Requests;
using Vennderful.Application.Features.Documents.Responses;
using Vennderful.Application.Features.Documents.Validators;
using Vennderful.Application.Features.Documents.DTOs;

namespace Vennderful.Application.Features.Documents.Handlers.Commands
{
    public class EditDocumentCommandHandler : IRequestHandler<EditDocumentCommand, EditDocumentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditDocumentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EditDocumentResponse> Handle(EditDocumentCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new EditDocumentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EditDocumentDto);

            var response = new EditDocumentResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var document = _mapper.Map<Document>(request.EditDocumentDto);
            if (document == null)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = new List<string> { "Document Not Found." };
                return response;
            }

            await _unitOfWork.NewDocumentRepository.UpdateAsync(document);

            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = new List<string> { "Bad Request." };

                return response;
            }

            response.Success = true;
            response.Message = "Updated Successfully.";
            response.Data = _mapper.Map<EditDocumentDto>(document);

            return response;
        }
    }
}
