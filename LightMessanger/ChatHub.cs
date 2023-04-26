using Microsoft.AspNetCore.SignalR;

namespace SignalRProject
{
    public class ChatHub : Hub
    {
        public async Task Connect()
        {
            // Add the user to a group based on their username
            var username = Context.User.Identity.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
        }

        public async Task SendMessageToUser(string username, string message)
        {
            // Send a message to a user by their username
            await Clients.Group(username).SendAsync("ReceiveMessage", message);
        }
    }
}
