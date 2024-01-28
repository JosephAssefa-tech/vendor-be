using System.Threading.Tasks;
using System.Threading;
using Vennderful.Domain.Entities;
using MediatR;
using Vennderful.Application.Features.EventFinance.Requests;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.EventFinance.Responses;
using Vennderful.Application.Features.EventFinance.Validators;
using Vennderful.Application.Features.EventFinance.Dto;
using System.Linq;

namespace Vennderful.Application.Features.EventFinance.Handlers.Commands
{
    public class CreateEventFinanceCommandHandler : IRequestHandler<CreateEventFinanceCommand, CreateEventFinanceResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEventFinanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateEventFinanceResponse> Handle(CreateEventFinanceCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventFinanceValidator();
            var validationResult = await validator.ValidateAsync(request.CreateEventFinanceDto);

            var response = new CreateEventFinanceResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }
                        
            var eventFinance = new Vennderful.Domain.Entities.EventFinance
            {
                EventId = request.CreateEventFinanceDto.EventId,
                PackageId = request.CreateEventFinanceDto.PackageId,
                PackagePrice = request.CreateEventFinanceDto.PackagePrice,
                DepositAmount = request.CreateEventFinanceDto.DepositAmount,
                DepositDueDate = request.CreateEventFinanceDto.DepositDueDate,
                TravelFees = request.CreateEventFinanceDto.TravelFees,
                DepositStatus=Domain.Enums.PaymentStatus.Pending,
            };
            eventFinance = await _unitOfWork.eventFinanceRepository.AddAsync(eventFinance);

            await _unitOfWork.Save();

            EventFinanceAddOn eventFinanceAddon = null;
            foreach(var addon in request.CreateEventFinanceDto.Addons) {
                eventFinanceAddon = new EventFinanceAddOn
                {
                    EventFinanceId = eventFinance.Id,
                    AddOnId = addon.Id,
                    Quantity = addon.Quantity,
                    TotalPrice = addon.TotalPrice
                };
                await _unitOfWork.eventFinanceAddOnRepository.AddAsync(eventFinanceAddon);
            }

            EventFinancePaymentSchedule eventFinancePaymentSchedule = null;
            foreach(var pay in request.CreateEventFinanceDto.Payments)
            {
                eventFinancePaymentSchedule = new EventFinancePaymentSchedule
                {
                    EventFinanceId = eventFinance.Id,
                    PaymentDate = pay.PaymentDate,
                    ScheduleAmount = pay.ScheduleAmount,
                };
                await _unitOfWork.eventFinancePaymentScheduleRepository.AddAsync(eventFinancePaymentSchedule);
            }

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateEventFinanceDto>(eventFinance);

            return response;
        }
    }
}
