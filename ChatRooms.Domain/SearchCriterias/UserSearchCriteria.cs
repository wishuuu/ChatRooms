namespace ChatRooms.Domain.SearchCriterias;

public class UserSearchCriteria : SearchCriteria<User>
{
    public string Nickname { get; set; }
    public string Email { get; set; }
}