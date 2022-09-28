using Bogus;
using ChatRooms.Domain;

namespace ChatRooms.FakeInfrastructure.Fakers;

public sealed class UserFaker : Faker<User>
{
    public UserFaker()
    {
        StrictMode(true);
        RuleFor(u => u.Id, f => f.IndexFaker);
        RuleFor(u => u.Nickname, f => f.Person.UserName);
        RuleFor(u => u.Password, f => f.Internet.Password());
        RuleFor(u => u.Email, (f, u) => u.Nickname + "@" + f.Internet.DomainName());
        RuleFor(u => u.Role, f => (UserRole)(f.Random.Bool(0.1f)? 1: 2));
        RuleFor(u => u.IsBanned, (f, u) => u.Role != UserRole.Admin && f.Random.Bool(0.1f));
        RuleFor(u => u.LastActivity, f => f.Date.Past());
        RuleFor(u => u.LastMessage, f => f.Date.Past());
        RuleFor(u => u.AllowMailing, f => f.Random.Bool());
    }
}