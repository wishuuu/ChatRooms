using ChatRooms.Domain;

namespace ChatRooms.Authorization;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public bool TryAuthorize(AuthModel authModel, out User? user)
    {
        var userTask = _userRepository.GetByNicknameAsync(authModel.Nickname);
        userTask.Wait();
        user = userTask.Result;
        if (user == null)
        {
            return false;
        }
    
        return user.Password == authModel.Password;
    }
}