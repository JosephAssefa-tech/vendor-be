using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventFinance.Dto;
using Vennderful.Application.Features.EventFinance.Requests;
using Vennderful.Application.Features.EventFinance.Responses;

namespace Vennderful.Application.Features.EventFinance.Handlers.Queries
{
    public class GetEventBudgetSummaryItemRequestHandler : IRequestHandler<GetEventBudgetSummaryItemRequest, GetEventBudgetSummaryItemResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEventBudgetSummaryItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetEventBudgetSummaryItemResponse> Handle(GetEventBudgetSummaryItemRequest request, CancellationToken cancellationToken)
        {
            var eventFinanceBudgetSummary = await _unitOfWork.eventFinanceRepository.GetEventFinanceBudgetSummaryItemByEventId(request.EventId, request.PackageId);
            var response = new GetEventBudgetSummaryItemResponse();

            if (eventFinanceBudgetSummary == null)
            {
                response.Success = false;
                response.Message = "The event finance budget summary item could not be found.";
                return response;
            }
            var eventFinanceBudgetSummaryItem = _mapper.Map<EventBudgetSummaryItemDto>(eventFinanceBudgetSummary);
            var eventFinanceBudgetSummaryAddOn = await _unitOfWork.eventFinanceAddOnRepository.GetEVentFinanceAddonsByEventFinanceIdAndAddonId(eventFinanceBudgetSummary.Id, request.AddonId);
            eventFinanceBudgetSummaryItem.AddonName = eventFinanceBudgetSummaryAddOn.AddOn.AddOnName;
            eventFinanceBudgetSummaryItem.PackageName= eventFinanceBudgetSummary.Package.PackageName;
            eventFinanceBudgetSummaryItem.AddonTotalPrice = eventFinanceBudgetSummaryAddOn.TotalPrice;


            response.Success = true;
            response.Data = eventFinanceBudgetSummaryItem;
            return response;

        }

    }
}
