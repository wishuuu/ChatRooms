namespace ChatRooms.Domain;

public class Room : BaseEntity
{
    public string Name { get; set; }
    public User Owner { get; set; }
    public List<User> Users { get; set; }
    public List<Message> Messages { get; set; }
}