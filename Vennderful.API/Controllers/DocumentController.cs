using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Vennderful.Application.Features.EventDocuments.Requests;
using Vennderful.Application.Features.EventDocuments.Responses;
using Vennderful.Application.Features.EventDocumentSignature.Dto;
using Vennderful.Application.Features.EventDocumentSigners.Requests;
using Vennderful.Application.Features.EventDocumentSigners.Responses;
using Vennderful.Application.Features.NewDocuments.DTOs;
using Vennderful.Application.Features.NewDocuments.Responses;
using Vennderful.Application.Features.EventDocumentSignature.Requests;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.EventDocumentSignature.Responses;
using Vennderful.Application.Features.Documents.Responses;
using Vennderful.Application.Features.Documents.DTOs;
using Vennderful.Application.Features.Documents.Requests;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{companyId}/Document", Name = ApiActions.GetNewDocuments)]
        public async Task<ActionResult<GetNewDocumentResponse>> GetListOfDocuments(string companyId)
        {
            var results = await _mediator.Send(new GetNewDocumentsRequest { CompanyId = Guid.Parse(companyId)});
            return Ok(results);
        }
       // [AllowAnonymous]
        // POST api/<NewDocumentController>
        [HttpPost("{companyId}/Document", Name = ApiActions.CreateNewDocument)]
            public async Task<ActionResult<CreateNewDocumentResponse>> CreateNewDocument([FromBody] CreateNewDocumentDto documentDto, string companyId)
        {
            documentDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateNewDocumentCommand { CreateNewDocumentDto = documentDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/NewDocument/{result.Data.CountDocuments}", UriKind.Relative),
                result.Data);
        }
        [HttpDelete("{companyId}/Document/{id}", Name = ApiActions.DeleteDocument)]
        public async Task<ActionResult<UpdateNewDocumentResponse>> DeleteDocument(Guid id, string companyId)
        {
            var command = new DeleteNewDocumentCommand { Id = id, companyId = Guid.Parse(companyId)};
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("{companyId}/Documet/{id}", Name = ApiActions.DownloadDocument)]
        [ProducesResponseType(typeof(NewDocumentDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetDownloadedDocumentResponse>> DownloadDocument(Guid id, string companyId)
        {
            var result = await _mediator.Send(new GetNewDocumentRequest { Id = id, CompanyId = Guid.Parse(companyId) });
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }


        [HttpGet("{companyId}/GetDocumentTemplates")]
        public async Task<ActionResult<GetDocumentTemplateListResponse>> GetAllDocuments([FromQuery] GetDocumentTemplateListQuery param, string companyId)
        {
            param.CompanyId = Guid.Parse(companyId);
            var result = await _mediator.Send(param);
            if (result.Errors != null && result.Errors.Count() > 0)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("{companyId}/GetDocumentTemplateNames")]
        public async Task<ActionResult<GetDocumentTemplateNameListResponse>> GetAllDocumentNames([FromQuery] GetDocumentTemplateNameListQuery param, string companyId)
        {
            param.CompanyId=Guid.Parse(companyId);
            var result = await _mediator.Send(param);
            if (result.Errors != null && result.Errors.Count() > 0)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("documents/{documentId}/eventdocuments/{eventdocumentId}/signers/{signerId}", Name = ApiActions.GetEventDocumentSigner)]
        public async Task<ActionResult<GetEventDocumentSignerResponse>> GetEventDocumentSigner([Required] Guid documentId, [Required] Guid eventdocumentId, [Required] Guid signerId)
        {
            var results = await _mediator.Send(new GetEventDocumentSignerRequest 
            {
                DocumentId = documentId,
                EventDocumentId = eventdocumentId,
                SignerId = signerId
            });
            return Ok(results);
        }

        [HttpPut("documents/{documentId}/eventdocuments/{eventdocumentId}/signers/{signerId}", Name = ApiActions.UpdateEventDocumentSigner)]
        public async Task<ActionResult<UpdateEventDocumentSignerResponse>> UpdateEventDocumentSigner([Required] Guid documentId, [Required] Guid eventdocumentId, [Required] Guid signerId)
        {
            var command = new UpdateEventDocumentSignerCommand 
            { 
                 DocumentId= documentId,
                 EventDocumentId = eventdocumentId,
                 SignerId = signerId
            };
            var result = await _mediator.Send(command);

            if (result?.Errors != null && result?.Errors.Count() > 0)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("{companyId}/{eventId}/EventDocument", Name = ApiActions.GetEventDocuments)]
        public async Task<ActionResult<GetEventDocumentResponse>> GetListOfEventDocuments(string eventId)
        {
            var results = await _mediator.Send(new GetEventDocumentsRequest { EventId=Guid.Parse(eventId) });
            return Ok(results);
        }
        [HttpPost("{companyId}/EventDocumentSignatureNotification", 
            Name = ApiActions.CreateEventDocumentSignatureNotification)]
        public async Task<ActionResult<CreateEventDocumentSignatureNotificationResponse>> CreateEventDocumentSignatureRequest([FromBody] CreateEventDocumentSignatureNotificationDto eventDocumentSignatureDto, string companyId)
        {
          //  eventDocumentSignatureDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateEventDocumentSignatureNotificationCommand{ CreateEventDocumentSignatureDto = eventDocumentSignatureDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/EventDocumentSignatureNotification/{result.Data}", UriKind.Relative),
                result.Data);
        }
        [HttpPost("{companyId}/CreateEventDocumentSigner", Name = ApiActions.CreateEventDocumentSigner)]
        public async Task<ActionResult<CreateEventDocumentSignatureResponse>> CreateEventDocumentSignature([FromBody] CreateEventDocumentSignatureDTO eventDocumentSignatureDto, string companyId)
        {
            //  eventDocumentSignatureDto.CompanyId = Guid.Parse(companyId);
            var command = new CreateEventDocumentSignerCommand { CreateEventDocumentSignatureDto = eventDocumentSignatureDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/EventDocumentSigner/{result.Data}", UriKind.Relative),
                result.Data);
        }

        [HttpGet("documents/{Id}/download", Name = ApiActions.DownloadDocumentFile)]
        public async Task<ActionResult> DownloadDocumentFile(Guid Id)
        {
            var document = await _mediator.Send(new DownloadDocumentRequest { Id = Id });

            if(document?.DocumentUrl != null && document?.DocumentUrl != string.Empty)
                return Redirect(document?.DocumentUrl);
            
            ChromePdfRenderer renderer = new ChromePdfRenderer();
            PdfDocument pdf = renderer.RenderHtmlAsPdf(document.DocumentBody);
            var bytes = pdf.BinaryData;
            
            
            return File(bytes, "application/pdf", document?.DocumentName);
        }

        [HttpPut("documents/{Id}", Name = ApiActions.EditDocument)]
        public async Task<ActionResult<EditDocumentResponse>> EditDocument([FromBody] EditDocumentDto editDocumentDto)
        {
            var command = new EditDocumentCommand
            {
                EditDocumentDto = editDocumentDto,
            };
            var result = await _mediator.Send(command);

            if (result?.Errors != null && result?.Errors.Count() > 0)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
