using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Infrastructure.Mail;
using MediatR;
using Vennderful.Application.Features.EventAndMember.Requests;
using Vennderful.Application.Features.EventAndMember.Responses;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventAndMember.Validators;
using System.Linq;
using Vennderful.Application.Features.EventAndMember.DTO;
using AutoMapper;
using System;

namespace Vennderful.Application.Features.EventAndMember.Handler.Commands
{
    public class CreateEventAndMemberCommandHandler : IRequestHandler<CreateEventAndMemberCommand, CreateEventAndMemberResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEventAndMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateEventAndMemberResponse> Handle(CreateEventAndMemberCommand request,
           CancellationToken cancellationToken)
        {

            var validator = new CreateEventAndMemberDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateEventAndMemberDTO);

            var response = new CreateEventAndMemberResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }
            
                var eventAndmember = _mapper.Map<Domain.Entities.EventAndMember>(request.CreateEventAndMemberDTO);
                eventAndmember.IsActive = false;
                eventAndmember = await _unitOfWork.eventAndMemberRepository.AddAsync(eventAndmember);
            await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Created Successfully.";
                response.Data = _mapper.Map<CreateEventAndMemberDTO>(eventAndmember);
                return response;
           
        }
    }
}
