namespace ChatRooms.Domain.DTOs;

public class MessageDTO : Base
{
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDTO Sender { get; set; }
}