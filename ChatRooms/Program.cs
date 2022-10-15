using System.Text;
using Bogus;
using ChatRooms.Application.Authorization;
using ChatRooms.Application.InvitationService;
using ChatRooms.Domain;
using ChatRooms.FakeInfrastructure;
using ChatRooms.FakeInfrastructure.Fakers;
using ChatRooms.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSingleton<Faker<User>, UserFaker>()
    .AddSingleton<Faker<Room>, RoomFaker>();

builder.Services
    .AddSingleton<IUserRepository, FakeUserRepository>()
    .AddSingleton<IRoomRepository, FakeRoomRepository>();

builder.Services
    .AddSingleton<IAuthService, AuthService>()
    .AddSingleton<ITokenService, JwtTokenService>()
    .AddSingleton<IRoomInvitationService, RoomInvitationService>();


string secretKey = "eyJhbGciOiJIUzI1NiJ9.ew0KICAic3ViIjogIjEyMzQ1Njc4OTAiLA0KICAibmFtZSI6ICJBbmlzaCBOYXRoIiwNCiAgImlhdCI6IDE1MTYyMzkwMjINCn0.0qnqLbn5A-pTPqD8k8Y-f0U6bo_tzF53ktJ-rpH41Ws";
string issuer = "ChatRooms";
var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = issuer
        };
    });

builder.Services.AddDataProtection()
    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });

builder.Services
    .Configure<FakeInfrastructureOptions>(builder.Configuration.GetSection("FakeInfrastructureOptions"))
    .Configure<RoomInvitationOptions>(builder.Configuration.GetSection("RoomInvitationOptions"));

string environmentName = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile("appsettings.json", false);
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", false);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "Bearer {token}"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/signalr");

app.Run();