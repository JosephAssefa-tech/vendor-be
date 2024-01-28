using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventTimeline.Requests;
using Vennderful.Application.Features.EventTimeline.Responses;
using Vennderful.Application.Features.EventTimeline.Validators;
using System.Linq;
using Vennderful.Application.Features.EventTimeline.DTOs;
using System.Collections.Generic;

namespace Vennderful.Application.Features.EventTimeline.Handlers.Commands
{
    public class CreateEventTimelineCommandHandler : IRequestHandler<CreateEventTimelineCommand, CreateEventTimelineResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEventTimelineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateEventTimelineResponse> Handle(CreateEventTimelineCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventTimelineValidator();
            var validationResult = await validator.ValidateAsync(request.CreateEventTimelineDto);

            var response = new CreateEventTimelineResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var eventTimeline = new Domain.Entities.EventTimeline
            {
                EventId = request.EventId,
                SlotTitle = request.CreateEventTimelineDto.SlotTitle,
                StartDate = request.CreateEventTimelineDto.StartDate,
                EndDate = request.CreateEventTimelineDto.EndDate,
                StartTime = request.CreateEventTimelineDto.StartTime,
                EndTime = request.CreateEventTimelineDto.EndTime,
                Comment = request.CreateEventTimelineDto.Comment,
                ResponsiblePersons = request.CreateEventTimelineDto.ResponsiblePersons
            };
            eventTimeline = await _unitOfWork.eventTimelineRepository.AddAsync(eventTimeline);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateEventTimelineDto>(eventTimeline);

            return response;
        }
    }
}
