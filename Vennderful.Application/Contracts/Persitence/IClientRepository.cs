using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IClientRepository : IAsyncRepository<Client>
    {
        Task<IEnumerable<Client>> GetClientByEmail(string email);
        Task<IEnumerable<Client>> GetInvitedClients(string companyId);
        Task<IEnumerable<Client>> GetClientsByName(string searchQuery, string companyId);
        Task<Client> GetClientById(string clientId);
    }
}
