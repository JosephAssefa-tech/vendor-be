using Microsoft.EntityFrameworkCore;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly VennderfulDbContext _dbContext;
        public RoomRepository(VennderfulDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Room>> GetAllRooms(Guid companyId)
        {
            return await(await GetQueryAsync(x => x.CompanyId == companyId)).ToListAsync();
        }
        public async Task<bool> CheckRoomNameExists(Guid companyId, string roomName)
        {
            var rooms = await GetAllRooms(companyId);
            return rooms.Any(room => room.RoomName == roomName);
        }

      
    }
}
