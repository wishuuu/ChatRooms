namespace ChatRooms.Domain.SearchCriterias;

public class RoomSearchCriteria : SearchCriteria<Room>
{
    public string? Name { get; }
    public int OwnerId { get; } = -1;
    public override bool Predicate(Room entity)
    {
        if (!base.Predicate(entity)) return false;
        if (!string.IsNullOrEmpty(Name) && !entity.Name.Contains(Name)) return false;
        if (OwnerId != -1 && entity.Owner.Id != OwnerId) return false;
        return true;
    }
}