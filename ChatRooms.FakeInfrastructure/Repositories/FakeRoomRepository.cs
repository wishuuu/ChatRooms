using Bogus;
using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.FakeInfrastructure;

public class FakeRoomRepository : FakeEntityRepository<Room>, IRoomRepository
{
    public FakeRoomRepository(Faker<Room> faker, FakeInfrastructureOptions options) : base(faker, options.RecordsCount.Rooms)
    {
    }
    
    public Task<IEnumerable<Room>> ListAsync(RoomSearchCriteria searchCriteria)
    {
        return Task.FromResult(Entities.Where(searchCriteria.Predicate));
    }

    public Task<IEnumerable<Message>> ListMessagesAsync(int roomId, int amount, int offset = 0)
    {
        return Task.FromResult(Entities.First(x => x.Id == roomId).Messages.Skip(offset).Take(amount));
    }

    
}