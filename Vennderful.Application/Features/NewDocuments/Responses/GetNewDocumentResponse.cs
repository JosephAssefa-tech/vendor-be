﻿using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.NewDocuments.DTOs;

namespace Vennderful.Application.Features.NewDocuments.Responses
{
    public class GetNewDocumentResponse : BaseResponse
    {
        public List<ListNewDocumentDto> Data { get; set; }
     
    }
}
