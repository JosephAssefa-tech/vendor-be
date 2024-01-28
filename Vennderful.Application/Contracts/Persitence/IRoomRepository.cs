using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IRoomRepository: IAsyncRepository<Room>
    {
        Task<IReadOnlyList<Room>> GetAllRooms(Guid companyId);
        Task<bool> CheckRoomNameExists(Guid companyId, string roomName);

    }
}
