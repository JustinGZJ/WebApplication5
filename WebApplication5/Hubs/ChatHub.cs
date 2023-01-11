using Microsoft.AspNetCore.SignalR;

namespace WebApplication5.Hubs;

public class ChatRoomHub : Hub
{
    public Task SendPublicMessage(string message)
    {
        string connId = this.Context.ConnectionId;
        string msg = $"{connId} {DateTime.Now}:{message}";
        return Clients.All.SendAsync("ReceivePublicMessage", msg);
    }
}