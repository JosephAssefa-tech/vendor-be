using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Infrastructure.Mail;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Member.Requests;
using Vennderful.Application.Features.Member.Responses;
using System.Linq;
using Vennderful.Application.Features.Member.Validators;
using Vennderful.Application.Features.Member.DTO;

namespace Vennderful.Application.Features.Member.Handler.Commands
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, CreateMemberResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateMemberResponse> Handle(CreateMemberCommand request,
           CancellationToken cancellationToken)
        {
            var validator = new CreateMemberDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateMemberDTO);

            var response = new CreateMemberResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }
            var member = _mapper.Map<Domain.Entities.Member>(request.CreateMemberDTO);
            member = await _unitOfWork.memberRepository.AddAsync(member);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateMemberDTO>(member);
            return response;
        }
    }
}
