using System;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface  IVenueAccountInformationRepository:IAsyncRepository<VenueAccountInformation>
    {
        Task<VenueAccountInformation> GetVenueByCompanyName(string companyName, Guid companyId);
        Task<VenueAccountInformation> GetById(Guid companyId);
    }
}
