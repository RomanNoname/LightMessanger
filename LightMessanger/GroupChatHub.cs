using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LightMessanger.WEB
{
    public class GroupChatHub : Hub
    {
        private IGroupMessagesService _groupMessagesService;
        private IUsersService _usersService;
        private IGroupsService _groupsService;
        public GroupChatHub(IGroupMessagesService groupMessagesService, IUsersService usersService, IGroupsService groupsService)
        {
            _groupMessagesService = groupMessagesService;
            _usersService = usersService;
            _groupsService = groupsService;
        }

        [Authorize]
        public async Task SendMessage(string message, string nameChat)
        {
            string sender = Context?.User?.Identity?.Name;
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await _usersService.GetValueByСonditionAsync(e => e.Name, Context.User.Identity.Name);
                var group = await _groupsService.GetValueByСonditionAsync(e => e.Name, nameChat);
                var newMessage = new GroupMessage()
                {
                    Content = message,
                    User = user,
                    ChatGroup = group
                };
                await _groupMessagesService.AddAsync(newMessage);
                await Clients.Group(nameChat).SendAsync("ReceiveMessage" + nameChat, message, sender);

            }

        }
        public override async Task OnConnectedAsync()
        {

            var userName = Context.User.Identity?.Name;

            //need short
            if (Context.GetHttpContext().Request.Cookies.ContainsKey("chat") &&
                (await _groupsService.GetGroupWithUsers(Context.GetHttpContext().Request.Cookies["chat"])).Users.Any(u => u.Name.Equals(userName)))
            {
                var user = await _usersService.GetUserGroupsAsync(e=>e.Name, userName); 
                foreach(var group in user.Groups)
                    await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);
            }    
             
        }

    }
}
