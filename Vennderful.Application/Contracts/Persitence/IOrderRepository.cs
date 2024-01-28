using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
    }
}
