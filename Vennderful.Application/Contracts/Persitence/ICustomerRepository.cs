using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    {
    }
}
