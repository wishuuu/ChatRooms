namespace ChatRooms.Domain;

public class User : BaseEntity
{
    public string Nickname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public bool IsBanned { get; set; }
}

public enum UserRole
{
    Admin,
    User
}