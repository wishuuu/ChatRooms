namespace ChatRooms.Domain.SearchCriterias;

public class RoomSearchCriteria : SearchCriteria<Room>
{
    public string? Name { get; }
    public int OwnerId { get; } = -1;
    public string? ParticipantName { get; set; }
    public override bool Predicate(Room entity)
    {
        if (!base.Predicate(entity)) return false;
        if (!string.IsNullOrEmpty(Name) && !entity.Name.Contains(Name)) return false;
        if (OwnerId != -1 && entity.Owner.Id != OwnerId) return false;
        if (!string.IsNullOrEmpty(ParticipantName) && entity.Users.All(p => p.Nickname != ParticipantName)) return false;
        return true;
    }
}