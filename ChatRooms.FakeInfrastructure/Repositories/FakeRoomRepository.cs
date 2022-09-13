using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.FakeInfrastructure;

public class FakeRoomRepository : FakeEntityRepository<Room>, IRoomRepository
{
    public Task<IReadOnlyList<Room>> ListAsync(RoomSearchCriteria searchCriteria)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Message>> ListMessagesAsync(int roomId, int amount, int offset = 0)
    {
        throw new NotImplementedException();
    }
}