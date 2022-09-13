using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.Domain;

public interface IEntityRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IReadOnlyList<TEntity>> ListAllAsync();
    Task<IReadOnlyList<TEntity>> ListAsync(SearchCriteria<TEntity> searchCriteria);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}