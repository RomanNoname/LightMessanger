using Microsoft.AspNetCore.SignalR;

namespace SignalRProject
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            string sender = Context?.User?.Identity?.Name;
            if(Context.User.Identity.IsAuthenticated)
                await this.Clients.All.SendAsync("ReceiveMessage", message, sender);
            else
                await this.Clients.All.SendAsync("ReceiveMessage", message, "Anonymous");
        }
        //public override async Task OnConnectedAsync()
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} вошел в чат");
        //    await base.OnConnectedAsync();
        //}
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} покинул в чат");
        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}
