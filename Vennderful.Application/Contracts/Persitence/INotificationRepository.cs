using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface INotificationRepository : IAsyncRepository<Notification>
    {
        Task<List<Notification>> GetNotificationsByUserId(Guid userId);
    }
}
