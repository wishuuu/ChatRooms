using ChatRooms.Application.Authorization;
using ChatRooms.Domain;
using ChatRooms.Hubs;
using Flurl.Http;
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Podaj swój login: ");
string login = Console.ReadLine();
Console.WriteLine("Podaj swoje hasło: ");
string password = Console.ReadLine();


var response = await "https://localhost:7158/auth/login"
    .PostJsonAsync(new AuthModel {Nickname = login, Password = password});

var token = await response.GetStringAsync();

var authorization = $"Bearer {token}";

HubConnection connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7158/signalr", opts =>
    {
        opts.HttpMessageHandlerFactory = (message) =>
        {
            if (message is HttpClientHandler clientHandler)
                // bypass SSL certificate
                clientHandler.ServerCertificateCustomValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => { return true; };
            return message;
        };
    })
    .WithAutomaticReconnect()
    .Build();
    
connection.On<Message, string>("GroupMessage", (message, group) => Console.WriteLine($"Wiadomość grupowa {group}: {message.Text}"));

Console.WriteLine($"Connecting...");
await connection.StartAsync();
Console.WriteLine($"Connected. {connection.ConnectionId}");
Console.WriteLine("Press CTRL+C to stop connection");

while (true)
{
    Console.WriteLine("Podaj wiadomość: ");
    var text = Console.ReadLine();
    Console.WriteLine("Podaj nazwę pokoju: ");
    var group = Console.ReadLine();
    var message = new Message();
    message.Text = text;
    message.Sender = new User {Nickname = login};

    await connection.SendAsync(nameof(ChatHub.SendGroupMessage), message, group);
}