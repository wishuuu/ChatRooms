using ChatRooms.Domain;

namespace ChatRooms.Authorization;

public interface ITokenService
{
    string CreateToken(User user);
}