using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.UploadDocuments.DTOs;
using Vennderful.Application.Features.UploadDocuments.Responses;
using Vennderful.Application.Models.UploadDocuments;

namespace Vennderful.Application.Features.UploadDocuments.Requests
{
    public class UploadImagesCommand: IRequest<UploadedImageUrlDTO>
    {
       public UploadImagesDto File { get; set; }
    }
}
