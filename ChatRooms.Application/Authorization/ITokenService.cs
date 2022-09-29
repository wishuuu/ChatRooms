using ChatRooms.Domain;

namespace ChatRooms.Application.Authorization;

public interface ITokenService
{
    string CreateToken(User user);
}