namespace ChatRooms.Domain;

public class Room : BaseEntity
{
    public string Name { get; set; }
    public User Owner { get; set; }
    public IEnumerable<User> Users { get; set; }
    public IEnumerable<Message> Messages { get; set; }
}