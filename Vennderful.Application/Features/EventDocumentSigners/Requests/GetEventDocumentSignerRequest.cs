using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Vennderful.Application.Features.EventDocumentSigners.Responses;

namespace Vennderful.Application.Features.EventDocumentSigners.Requests
{
    public class GetEventDocumentSignerRequest : IRequest<GetEventDocumentSignerResponse>
    {
        [Required]
        public Guid DocumentId { get; set; }

        [Required]
        public Guid EventDocumentId { get; set; }

        [Required]
        public Guid SignerId { get; set; }
    }
}
