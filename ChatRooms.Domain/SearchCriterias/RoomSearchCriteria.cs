namespace ChatRooms.Domain.SearchCriterias;

public class RoomSearchCriteria : SearchCriteria<Room>
{
    public string Name { get; set; }
    public int OwerId { get; set; }
}