using Bogus;
using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;
using Microsoft.Extensions.Options;

namespace ChatRooms.FakeInfrastructure;

public class FakeUserRepository : FakeEntityRepository<User>, IUserRepository
{
    public FakeUserRepository(Faker<User> faker, IOptions<FakeInfrastructureOptions> options) : base(faker, 10) // TODO FIX IT
    {
    }
    public Task<IEnumerable<User>> ListAsync(UserSearchCriteria searchCriteria)
    {
        return Task.FromResult(Entities.Where(searchCriteria.Predicate));
    }

    
}