using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;

namespace Vennderful.Application.Features.Events.Handlers.Commands
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, DeleteEventResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DeleteEventResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.eventRepository.GetById(request.Id);

            var response = new DeleteEventResponse();

            if (result == null)
            {
                response.Success = false;
                response.Message = "Deletion Failed.";
                response.Errors = new List<string> { "Event Not Found." };
                return response;
            }

            await _unitOfWork.eventRepository.DeleteAsync(result);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Deleted Successfully.";

            return response;
        }



    }
}
