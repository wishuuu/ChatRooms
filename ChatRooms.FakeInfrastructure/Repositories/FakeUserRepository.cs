using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.FakeInfrastructure;

public class FakeUserRepository : FakeEntityRepository<User>, IUserRepository
{
    public Task<IReadOnlyList<User>> ListAsync(UserSearchCriteria spec)
    {
        throw new NotImplementedException();
    }
}