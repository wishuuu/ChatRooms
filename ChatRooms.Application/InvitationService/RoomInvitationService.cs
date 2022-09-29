using ChatRooms.Domain;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ChatRooms.Application.InvitationService;

public class RoomInvitationService : IRoomInvitationService
{
    private readonly IOptions<RoomInvitationOptions> _options;
    private readonly IDataProtectionProvider _dataProtectionProvider;

    public RoomInvitationService(IOptions<RoomInvitationOptions> options, IDataProtectionProvider dataProtectionProvider)
    {
        _options = options;
        _dataProtectionProvider = dataProtectionProvider;
    }
    
    public string CreateInvitation(int roomId, int? userId = null)
    {
        var invitation = new RoomInvitation(roomId, userId, DateTime.Now + _options.Value.expirationTimeSpan);
        
        var serialized = JsonConvert.SerializeObject(invitation);

        return _dataProtectionProvider.CreateProtector(_options.Value.secretKey).Protect(serialized);
    }

    public bool ResolveInvitation(string invitationString, out RoomInvitation? invitation)
    {
        var serialized = _dataProtectionProvider.CreateProtector(_options.Value.secretKey).Unprotect(invitationString);
        invitation = JsonConvert.DeserializeObject<RoomInvitation>(serialized);
        
        if(invitation == null || invitation.Expiration < DateTime.Now)
            return false;
        return true;
    }
}