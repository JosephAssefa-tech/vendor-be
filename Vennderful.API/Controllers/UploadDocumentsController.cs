using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Contracts.BlobStorage.Blob;
using Vennderful.Application.Features.UploadDocuments.Requests;
using Vennderful.Application.Models.UploadDocuments;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class UploadDocumentsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IDocumentUploadStorageService _uploadService;

        public UploadDocumentsController(IMediator mediator, IDocumentUploadStorageService uploadService)
        {
            _mediator = mediator;
            _uploadService = uploadService;
        }

        [HttpPost("{companyId}/{category}/uploadImage")]
        public async Task<IActionResult> UploadImages(IFormFile formFile, string companyId,string category)
        {            
            var uploadImagesCommand= new UploadImagesCommand();
                var file = new UploadImagesDto
                {
                    Content = formFile.OpenReadStream(),
                    Name = formFile.FileName,
                    ContentType = formFile.ContentType,
                    Category = category,
                    CompanyId = companyId
                };
                uploadImagesCommand.File = file;
            var response = await _mediator.Send(uploadImagesCommand);
            return Ok(response);
           
        }
        [HttpPost("{companyId}/UploadDocument")]
        public async Task<IActionResult> UploadDocument([FromForm] DocumentRequest request, string companyId)
        {
            request.CompanyId = Guid.Parse(companyId);
            var response = await _mediator.Send(request);
            return Ok(response);
        }



    }
}
