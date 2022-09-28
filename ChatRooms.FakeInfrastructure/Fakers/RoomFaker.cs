using Bogus;
using ChatRooms.Domain;

namespace ChatRooms.FakeInfrastructure.Fakers;

public sealed class RoomFaker : Faker<Room>
{
    public RoomFaker(IUserRepository userRepository)
    {
        var usersTask = userRepository.ListAllAsync();
        usersTask.Wait();
        var users = usersTask.Result;

        StrictMode(true);
        RuleFor(r => r.Id, f => f.IndexFaker);
        RuleFor(r => r.Name, f => f.Lorem.Word());
        RuleFor(r => r.Owner, f => f.PickRandom(users));
        RuleFor(r => r.Users, (f, r)=> f.PickRandom(users.Where(u => u.Id != r.Owner.Id), 3).Concat(new []{ r.Owner }).ToList());
        RuleFor(r => r.Messages, (f, r) =>new MessageFaker(r.Users).Generate(10));
    }
}