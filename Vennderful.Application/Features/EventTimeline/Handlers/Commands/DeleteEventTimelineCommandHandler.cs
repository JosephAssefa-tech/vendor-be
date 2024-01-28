using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.EventTimeline.Requests;
using Vennderful.Application.Features.EventTimeline.Responses;
using Vennderful.Application.Contracts.Persitence;

namespace Vennderful.Application.Features.EventTimeline.Handlers.Commands
{
    public class DeleteEventTimelineCommandHandler : IRequestHandler<DeleteEventTimelineCommand, DeleteEventTimelineResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventTimelineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteEventTimelineResponse> Handle(DeleteEventTimelineCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.eventTimelineRepository.GetByIdAsync(request.Id);

            var response = new DeleteEventTimelineResponse();

            if (result == null)
            {
                response.Success = false;
                response.Message = "Deletion Failed.";
                response.Errors = new List<string> { "Timeline Record Not Found." };
                return response;
            }

            await _unitOfWork.eventTimelineRepository.DeleteAsync(result);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Deleted Successfully.";

            return response;
        }
    }
}
