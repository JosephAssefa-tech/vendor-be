using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Vennderful.Application.Features.EventTimeline.Responses;
using Vennderful.Application.Features.EventTimeline.Requests;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using System.Linq;
using Vennderful.Application.Features.EventTimeline.DTOs;

namespace Vennderful.Application.Features.EventTimeline.Handlers.Queries
{
    public class GetEventTimelinesRequestHandler : IRequestHandler<GetEventTimelinesRequest, GetEventTimelinesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventTimelinesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetEventTimelinesResponse> Handle(GetEventTimelinesRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetEventTimelinesResponse();
            try
            {
                var eventTimelines = (await _unitOfWork.eventTimelineRepository.GetEventTimelineByEventId(request.EventId)).ToList();

                response.Success = true;
                response.Data = _mapper.Map<List<EventTimelineDto>>(eventTimelines);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong.";
                response.Data = new List<EventTimelineDto>();
                response.Errors = new List<string>() { ex.Message };

                return response;
            }
        }
    }
}
