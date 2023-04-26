using Microsoft.AspNetCore.SignalR;

namespace LightMessanger.WEB
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity.Name;
        }
    }
}
