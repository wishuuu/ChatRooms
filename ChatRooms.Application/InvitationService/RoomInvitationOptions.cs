namespace ChatRooms.Application.InvitationService;

public class RoomInvitationOptions
{
    public string secretKey { get; set; }
    public TimeSpan expirationTimeSpan { get; set; }
}