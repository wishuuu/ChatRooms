using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.Domain;

public interface IRoomRepository : IEntityRepository<Room>
{
    Task<IEnumerable<Room>> ListAsync(RoomSearchCriteria searchCriteria);
    Task<IEnumerable<Message>> ListMessagesAsync(int roomId, int amount, int offset=0);
}