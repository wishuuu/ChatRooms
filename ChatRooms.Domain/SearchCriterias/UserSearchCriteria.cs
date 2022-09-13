namespace ChatRooms.Domain.SearchCriterias;

public class UserSearchCriteria : SearchCriteria<User>
{
    public string Nickname { get; set; }
    public string Email { get; set; }
    public override bool Predicate(User entity)
    {
        if (!base.Predicate(entity)) return false;
        if (!string.IsNullOrEmpty(Nickname) && !entity.Nickname.Contains(Nickname)) return false;
        if (!string.IsNullOrEmpty(Email) && !entity.Email.Contains(Email)) return false;
        return true;
    }
}