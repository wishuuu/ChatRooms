using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.Domain;

public interface IRoomRepository : IEntityRepository<Room>
{
    Task<IReadOnlyList<Room>> ListAsync(RoomSearchCriteria searchCriteria);
    Task<IReadOnlyList<Message>> ListMessagesAsync(int roomId, int amount, int offset=0);
}