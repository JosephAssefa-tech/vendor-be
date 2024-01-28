using System.Threading.Tasks;
using System;
using Vennderful.Domain.Entities;
using System.Collections.Generic;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IEventFinanceAddOnRepository : IAsyncRepository<EventFinanceAddOn>
    {
        Task<List<EventFinanceAddOn>> GetEventFinanceAddOnByEventFinanceId(Guid eventFinanceId);
        Task<EventFinanceAddOn> GetEVentFinanceAddonsByEventFinanceIdAndAddonId(Guid eventFinanceId, Guid addOnId);

    }
}
