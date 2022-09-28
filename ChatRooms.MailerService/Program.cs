using Bogus;
using ChatRooms.Domain;
using ChatRooms.FakeInfrastructure;
using ChatRooms.FakeInfrastructure.Fakers;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

IServiceCollection services = new ServiceCollection()
    .AddSingleton<Faker<User>, UserFaker>()
    .AddSingleton<IUserRepository, FakeUserRepository>();

services.AddOptions<FakeInfrastructureOptions>("appsettings.json");

var provider = services.BuildServiceProvider();

var usersToMail = (await provider.GetRequiredService<IUserRepository>().ListAllAsync()).Where(u => u.AllowMailing == false && u.LastActivity < u.LastMessage);

foreach (var user in usersToMail)
{
    // TODO Send mails to users
}