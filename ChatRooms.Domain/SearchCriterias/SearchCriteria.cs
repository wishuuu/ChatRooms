namespace ChatRooms.Domain.SearchCriterias;

public abstract class SearchCriteria<TEntity> where TEntity : BaseEntity
{
    public int Id { get; set; } = -1;
    public int IdFrom { get; set; } = -1;
    public int IdTo { get; set; } = -1;
}