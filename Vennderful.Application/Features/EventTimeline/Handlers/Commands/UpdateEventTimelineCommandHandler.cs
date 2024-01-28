using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventTimeline.Requests;
using Vennderful.Application.Features.EventTimeline.Responses;
using Vennderful.Domain.Entities;
using Vennderful.Application.Features.EventTimeline.Validators;
using System.Linq;
using Vennderful.Application.Features.EventTimeline.DTOs;

namespace Vennderful.Application.Features.EventTimeline.Handlers.Commands
{
    public class UpdateEventTimelineCommandHandler : IRequestHandler<UpdateEventTimelineCommand, UpdateEventTimelineResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEventTimelineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateEventTimelineResponse> Handle(UpdateEventTimelineCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEventTimelineValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateEventTimelineDto);

            var response = new UpdateEventTimelineResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Update Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var existingEventTimeline = await _unitOfWork.eventTimelineRepository.GetByIdAsync(request.UpdateEventTimelineDto.Id);

            var updatedEventTimeline = _mapper.Map<Domain.Entities.EventTimeline>(request.UpdateEventTimelineDto);

            if (existingEventTimeline != null)
            {
                existingEventTimeline.SlotTitle = updatedEventTimeline.SlotTitle;
                existingEventTimeline.StartDate = updatedEventTimeline.StartDate;
                existingEventTimeline.EndDate = updatedEventTimeline.EndDate;
                existingEventTimeline.StartTime = updatedEventTimeline.StartTime;
                existingEventTimeline.EndTime = updatedEventTimeline.EndTime;
                existingEventTimeline.Comment = updatedEventTimeline.Comment;
                existingEventTimeline.ResponsiblePersons = updatedEventTimeline.ResponsiblePersons;

                await _unitOfWork.eventTimelineRepository.UpdateAsync(existingEventTimeline);

            }
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Updated Successfully.";
            response.Data = _mapper.Map<UpdateEventTimelineDto>(existingEventTimeline);

            return response;
        }
    }
}
