namespace ChatRooms.Domain;

public class RoomInvitation
{
    public int RoomId;
    public int? UserId;
    public DateTime Expiration;
    
    public RoomInvitation(int roomId, int? userId, DateTime expiration)
    {
        RoomId = roomId;
        UserId = userId;
        Expiration = expiration;
    }
}