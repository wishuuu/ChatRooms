using Bogus;
using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.FakeInfrastructure;

public class FakeUserRepository : FakeEntityRepository<User>, IUserRepository
{
    public FakeUserRepository(Faker<User> faker, FakeInfrastructureOptions options) : base(faker, options.RecordsCount.Users)
    {
    }
    public Task<IEnumerable<User>> ListAsync(UserSearchCriteria searchCriteria)
    {
        return Task.FromResult(Entities.Where(searchCriteria.Predicate));
    }

    
}