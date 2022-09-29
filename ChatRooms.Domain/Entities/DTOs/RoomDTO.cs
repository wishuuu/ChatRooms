namespace ChatRooms.Domain.DTOs;

public class RoomDTO : BaseEntity
{
    public string Name { get; set; }
    public UserDTO Owner { get; set; }
    public List<UserDTO> Users { get; set; }
}