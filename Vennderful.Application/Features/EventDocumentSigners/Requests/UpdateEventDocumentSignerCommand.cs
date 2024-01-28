using MediatR;
using System.ComponentModel.DataAnnotations;
using System;
using Vennderful.Application.Features.EventDocumentSigners.Responses;

namespace Vennderful.Application.Features.EventDocumentSigners.Requests
{
    public class UpdateEventDocumentSignerCommand : IRequest<UpdateEventDocumentSignerResponse>
    {
        [Required]
        public Guid DocumentId { get; set; }

        [Required]
        public Guid EventDocumentId { get; set; }

        [Required]
        public Guid SignerId { get; set; }
    }
}
