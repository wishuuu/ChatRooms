using System.Collections.ObjectModel;
using Bogus;
using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.FakeInfrastructure;

public abstract class FakeEntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
{
    protected IEnumerable<TEntity> Entities;
    protected FakeEntityRepository(Faker<TEntity> faker, int count)
    {
        Entities = faker.Generate(count);
    }

    public Task<int> GetNextIdAsync()
    {
        try
        {
            return Task.FromResult(Entities.Max(e => e.Id) + 1);
        }
        catch (InvalidOperationException e)
        {
            return Task.FromResult(1);
        }
    }
    public Task<TEntity?> GetByIdAsync(int id)
    {
        return Task.FromResult(Entities.Where(x => x.Id == id).DefaultIfEmpty(null).First());
    }

    public Task<IEnumerable<TEntity>> ListAllAsync()
    {
        return Task.FromResult(Entities);
    }

    public Task<IEnumerable<TEntity>> ListAsync(SearchCriteria<TEntity> searchCriteria)
    {
        return Task.FromResult(Entities.Where(searchCriteria.Predicate));
    }

    public Task<TEntity> AddAsync(TEntity entity)
    {
        return Task.FromResult(Entities.Append(entity).Last());
    }

    public Task UpdateAsync(TEntity entity)
    {
        Entities = Entities.Select(e => e.Id == entity.Id ? entity : e);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity)
    {
        Entities = Entities.Where(e => e.Id != entity.Id);
        return Task.CompletedTask;
    }
}