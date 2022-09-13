using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.FakeInfrastructure;

public class UserRepository : EntityRepository<User>, IUserRepository
{
    public Task<IReadOnlyList<User>> ListAsync(UserSearchCriteria spec)
    {
        throw new NotImplementedException();
    }
}