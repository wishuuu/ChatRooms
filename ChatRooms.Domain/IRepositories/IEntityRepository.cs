using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.Domain;

public interface IEntityRepository<TEntity> where TEntity : BaseEntity
{
    Task<int> GetNextIdAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> ListAllAsync();
    Task<IEnumerable<TEntity>> ListAsync(SearchCriteria<TEntity> searchCriteria);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}