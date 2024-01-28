using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.WorkingHours.DTOs;
using Vennderful.Application.Features.WorkingHours.Requests;
using Vennderful.Application.Features.WorkingHours.Responses;
using Vennderful.Application.Features.WorkingHours.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.WorkingHours.Handlers.Commands
{
    public class CreateWorkingHourHandler : IRequestHandler<CreateWorkingHourCommand, CreateWorkingHourResponse>
    {
     
             private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWorkingHourHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateWorkingHourResponse> Handle(CreateWorkingHourCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateWorkingHourDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateWorkingHourDto);

            var response = new CreateWorkingHourResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var workingHour = _mapper.Map<WorkingHour>(request.CreateWorkingHourDto);
            workingHour = await _unitOfWork.workingHoursRepository.AddAsync(workingHour);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateWorkingHourDto>(workingHour);

            return response;
        }
    }
}
