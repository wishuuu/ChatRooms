using System.Security.Claims;
using ChatRooms.Domain;

namespace ChatRooms.Application.Authorization;

public static class AuthFunc
{
    public static bool EnsureUserIsInRoom(ClaimsIdentity? identity, Room room, out int userId)
    {
        userId = -1;
        if (identity != null)
        {
            IEnumerable<Claim> claims = identity.Claims;

            if (!int.TryParse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value, out userId))
                return false;

            var i = userId;
            if (room.Users.All(u => u.Id != i))
                return false;
        }
        return false;
    }
    
    public static bool EnsureUserIsRoomOwner(ClaimsIdentity? identity, Room room, out int userId)
    {
        userId = -1;
        if (identity != null)
        {
            IEnumerable<Claim> claims = identity.Claims;

            if (!int.TryParse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value, out userId))
                return false;
            
            if (room.Owner.Id != userId)
                return false;
        }
        return false;
    }
    
}