using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;
using Vennderful.Application.Contracts.Persitence;
using Microsoft.EntityFrameworkCore;

namespace Vennderful.Persistence.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(VennderfulDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Client>> GetClientByEmail(string email)
        {
            var clientList = (await GetQueryAsync(x => x.Email != null && x.Email.ToLower() == email.ToLower())).ToList();
            return clientList;
        }

        public async Task<Client> GetClientById(string clientId)
        {
            var client = (await GetQueryAsync(x => x.Id == Guid.Parse(clientId))).Include(x => x.EventClients).ThenInclude(y => y.Event).FirstOrDefault();
            return client;
        }

        public async Task<IEnumerable<Client>> GetInvitedClients(string companyId)
        {
            throw new NotImplementedException();
            //var inviteeList = (await GetQueryAsync(x => x.CompanyId == Guid.Parse(companyId) && x.Status == "Invited")).OrderByDescending(x => x.Created).ToList();
            //return inviteeList;
        }

        public async Task<IEnumerable<Client>> GetClientsByName(string searchQuery, string companyId)
        {
            var searchQueryLower = searchQuery.ToLower();
            var clientList = (await GetQueryAsync(x =>
                (x.FirstName != null && x.FirstName.ToLower().Contains(searchQueryLower)) ||
                (x.LastName != null && x.LastName.ToLower().Contains(searchQueryLower)) ||
                (x.Email != null && x.Email.ToLower().Contains(searchQueryLower))
            ))
            .OrderByDescending(x => x.Created)
            .ToList();
            return clientList;
        }
    }
}
