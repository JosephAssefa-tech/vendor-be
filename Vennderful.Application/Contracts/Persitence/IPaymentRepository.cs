using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IPaymentRepository : IAsyncRepository<Vennderful.Domain.Entities.Payment>
    {
    }
}
