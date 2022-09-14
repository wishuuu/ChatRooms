using Bogus;
using ChatRooms.Domain;
using ChatRooms.FakeInfrastructure;
using ChatRooms.FakeInfrastructure.Fakers;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSingleton<Faker<User>, UserFaker>()
    .AddSingleton<Faker<Room>, RoomFaker>();

builder.Services
    .AddSingleton<IUserRepository, FakeUserRepository>()
    .AddSingleton<IRoomRepository, FakeRoomRepository>();

// builder.Services.Configure<IOptions<FakeInfrastructureOptions>>(builder.Configuration.GetSection("FakeInfrastructureOptions"));

string environmentName = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile("appsettings.json", false);
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", false);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();