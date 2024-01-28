using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vennderful.Application.Features.UploadDocuments.DTOs;
using Vennderful.Application.Features.UploadDocuments.Requests;
using Vennderful.Application.Models.UploadDocuments;

namespace Vennderful.Application.Features.UploadDocuments.Validators
{
    public class UploadImagesCommandHandler : AbstractValidator<UploadImagesCommand>
    {
        public UploadImagesCommandHandler()
        {
            RuleFor(doc => doc.File)
                .Must(FilesNotEmpty)
                .WithMessage("Files can't be empty");

            RuleFor(v => v.File)
                .Must(IsValidContentType)
                .WithMessage("Invalid file type. only '.jpg' and '.png' files are allowed");

        }

        private bool FilesNotEmpty(UploadImagesDto image)
        {
            if (image == null)
            {
                return false;
            }
            if (image.Content.Length == 0)
            {
                return false;
            }
            return true;
        }

        private bool IsValidContentType(UploadImagesDto image)
        {
            var validDocumentTypes = new string[] { "image/jpeg", "image/png" };
            if (!validDocumentTypes.Contains(image.ContentType))
            {
                return false;
            }
            return true;
        }
    }
}
