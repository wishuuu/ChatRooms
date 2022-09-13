namespace ChatRooms.Domain;

public class Message : BaseEntity
{
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public User Sender { get; set; }
    public Room ChatRoom { get; set; }
}