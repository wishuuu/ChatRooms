namespace ChatRooms.Domain.SearchCriterias;

public abstract class SearchCriteria<TEntity> where TEntity : BaseEntity
{
    public int Id { get; } = -1;
    public int IdFrom { get; } = -1;
    public int IdTo { get; } = -1;

    public virtual bool Predicate(TEntity entity)
    {
        if (Id != -1 && entity.Id != Id)
            return false;
        if (IdFrom != -1 && entity.Id <= IdFrom)
            return false;
        if (IdTo != -1 && entity.Id >= IdTo)
            return false;
        return true;
    }
}