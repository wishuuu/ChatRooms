namespace ChatRooms.Domain;

public class Message : Base
{
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public User? Sender { get; set; }
}