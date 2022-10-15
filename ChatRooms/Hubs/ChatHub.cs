using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatRooms.Hubs;

public class ChatHub : Hub
{
    private readonly IRoomRepository _roomRepository;
    
    public ChatHub(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task SendGroupMessage(Message message, string roomName)
    {
        await Clients.Group(roomName).SendAsync("GroupMessage", message, roomName);
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"Connected: {Context.ConnectionId}");
        Console.WriteLine(Context.UserIdentifier);
        Console.WriteLine(Context.User?.Identity?.Name);
        
        var _ = JoinRooms();
        
        
        return base.OnConnectedAsync();
    }

    private async Task JoinRooms()
    {
        var roomsToJoin = await _roomRepository.ListAsync(new RoomSearchCriteria
        {
            ParticipantName = Context.User?.Claims.FirstOrDefault(c => c.Type == "name")?.Value
        });

        foreach (var room in roomsToJoin)
        {
            var _ = Groups.AddToGroupAsync(Context.ConnectionId, room.Name);
        }
    }
}