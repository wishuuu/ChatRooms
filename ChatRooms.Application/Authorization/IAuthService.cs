using ChatRooms.Domain;

namespace ChatRooms.Application.Authorization;

public interface IAuthService
{
    bool TryAuthorize(AuthModel authModel, out User? user);
}