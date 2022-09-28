namespace ChatRooms.Domain.DTOs;

public class UserDTO : BaseEntity
{
    public string Nickname { get; set; }
    public string Email { get; set; }
}