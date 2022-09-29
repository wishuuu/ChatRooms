using ChatRooms.Application.InvitationService;
using ChatRooms.Domain;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace TestProject1.Services;

[TestFixture]
public class RoomInvitationServiceTestFixture
{
    private IRoomInvitationService _roomInvitationService;
    
    
    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        
        services.AddDataProtection().UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
        {
            EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
            ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
        });
        
        services.Configure<RoomInvitationOptions>(options =>
        {
            options.expirationTimeSpan = TimeSpan.FromMinutes(10);
            options.secretKey = "dupa1234dupa1234";
        });

        services.AddSingleton<IRoomInvitationService, RoomInvitationService>();
        
        var serviceProvider = services.BuildServiceProvider();
        
        _roomInvitationService = serviceProvider.GetRequiredService<IRoomInvitationService>();
    }
    
    [Test]
    public void Test()
    {
        RoomInvitation? expected = new RoomInvitation(1, 1, DateTime.Now);
        var invitation = _roomInvitationService.CreateInvitation(1, 1);
        var success = _roomInvitationService.ResolveInvitation(invitation, out RoomInvitation? decryptedRoom);
        
        Assert.That(success, Is.True);
        
        expected.Expiration = decryptedRoom!.Expiration;
        
        AssertExt.AreEqualByJson(decryptedRoom, expected);
    }
}