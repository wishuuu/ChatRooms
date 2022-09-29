using ChatRooms.Domain;

namespace ChatRooms.Application.InvitationService;

public interface IRoomInvitationService
{
    string CreateInvitation(int roomId, int? userId = null);
    bool ResolveInvitation(string invitationString, out RoomInvitation? invitation);
}