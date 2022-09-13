using Bogus;
using ChatRooms.Domain;

namespace ChatRooms.FakeInfrastructure.Fakers;

public sealed class MessageFaker : Faker<Message>
{
    public MessageFaker(IReadOnlyList<User> usersPossible)
    {
        StrictMode(false);
        RuleFor(m => m.Text, f => f.Lorem.Sentence());
        RuleFor(m => m.CreatedAt, f => f.Date.Past(4));
        RuleFor(m => m.Sender, f => f.PickRandom<User>(usersPossible));
    }
}