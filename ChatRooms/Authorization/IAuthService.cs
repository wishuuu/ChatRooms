using ChatRooms.Domain;

namespace ChatRooms.Authorization;

public interface IAuthService
{
    bool TryAuthorize(AuthModel authModel, out User? user);
}