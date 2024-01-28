using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(VennderfulDbContext dbContext) : base(dbContext) { }

        public async Task<List<Notification>> GetNotificationsByUserId(Guid userId)
        {
            var notifications = (await GetQueryAsync(x => x.UserId != Guid.Empty && x.UserId == userId && x.HasBeenRead == false)).ToList();
            return notifications;
        }
    }
}
