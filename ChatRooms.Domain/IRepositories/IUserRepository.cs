using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.Domain;

public interface IUserRepository : IEntityRepository<User>
{
    Task<IReadOnlyList<User>> ListAsync(UserSearchCriteria spec);
}