using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.FakeInfrastructure;

public class FakeEntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TEntity>> ListAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TEntity>> ListAsync(SearchCriteria<TEntity> searchCriteria)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}