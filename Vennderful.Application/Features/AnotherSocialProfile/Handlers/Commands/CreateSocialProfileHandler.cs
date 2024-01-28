using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.AnotherSocialProfile.DTOs;
using Vennderful.Application.Features.AnotherSocialProfile.Requests;
using Vennderful.Application.Features.AnotherSocialProfile.Responses;
using Vennderful.Application.Features.AnotherSocialProfile.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.AnotherSocialProfile.Handlers.Commands
{
    public class CreateSocialProfileHandler : IRequestHandler<CreateSocialProfileCommand, CreateSocialProfileResponse>
    {
     
             private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSocialProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateSocialProfileResponse> Handle(CreateSocialProfileCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateSocialProfileDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateSocialProfileDto);

            var response = new CreateSocialProfileResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var profile = _mapper.Map<SocialProfile>(request.CreateSocialProfileDto);
            profile = await _unitOfWork.socialProfileRepository.AddAsync(profile);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateSocialProfileDto>(profile);

            return response;
        }
    }
}
