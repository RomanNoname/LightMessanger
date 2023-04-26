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
        private IUnreadMessagesService _unreadMessagesService;
        private static Dictionary<string, List<string>> _groupUsers = new Dictionary<string, List<string>>();
        public GroupChatHub(IGroupMessagesService groupMessagesService, IUsersService usersService, IGroupsService groupsService, IUnreadMessagesService unreadMessagesService)
        {
            _groupMessagesService = groupMessagesService;
            _usersService = usersService;
            _unreadMessagesService = unreadMessagesService;
            _groupsService = groupsService;

        }

        [Authorize]
        public async Task SendMessage(string message, string nameChat)
        {
            string sender = Context?.User?.Identity?.Name;
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await _usersService.GetValueByСonditionAsync(e => e.Name, Context.User.Identity.Name);
                var group = await _groupsService.GetGroupWithUsers(nameChat);
                var newMessage = new GroupMessage()
                {
                    Content = message,
                    User = user,
                    ChatGroup = group
                };
                await _groupMessagesService.AddAsync(newMessage);
                await Clients.Group(nameChat).SendAsync("ReceiveMessage" + nameChat, message, sender);
                foreach (var item in group.Users)
                {
                    await AddUnreadMessagesToDisconnectedUsers(group.Name);
                    await Clients.Group(item.Name).SendAsync("Notifications", group.Name, sender);
                }

            }

        }
        public override async Task OnConnectedAsync()
        {

            var userName = Context.User.Identity?.Name;

            //need short
            if (Context.GetHttpContext().Request.Cookies.ContainsKey("chat") &&
                (await _groupsService.GetGroupWithUsers(Context.GetHttpContext().Request.Cookies["chat"])).Users.Any(u => u.Name.Equals(userName)))
            {
                await AddUserToGroup(userName, Context.GetHttpContext().Request.Cookies["chat"]);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, userName);

        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.GetHttpContext().Request.Cookies.ContainsKey("chat"))
                await DeleteUserFromGroup(Context.User.Identity.Name, Context.GetHttpContext().Request.Cookies["chat"]);
            await Groups.RemoveFromGroupAsync(Context.User.Identity?.Name, Context.ConnectionId);
        }
        private async Task AddUserToGroup(string userName, string chatName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
            if (!_groupUsers.ContainsKey(chatName))
                _groupUsers.Add(chatName, new List<string> { userName });
            else
                if (!_groupUsers[chatName].Contains(userName))
                    _groupUsers[chatName].Add(userName);
        }
        private async Task DeleteUserFromGroup(string userName, string chatName)
        {
            await Groups.RemoveFromGroupAsync(chatName, Context.ConnectionId);
            if(_groupUsers.ContainsKey(chatName))
                _groupUsers[chatName].Remove(userName);
        }
        private async Task AddUnreadMessagesToDisconnectedUsers(string chatName)
        {
            List<User> users = (await _groupsService.GetGroupWithUsers(chatName)).Users.Where(u=>!_groupUsers[chatName].Contains(u.Name)).ToList();
            Group group = await _groupsService.GetValueByСonditionAsync(u => u.Name, chatName);
            foreach (User user in users)
            {
                var unread = new UnreadMessages()
                {
                    User = user,
                    Group = group
                };
                await _unreadMessagesService.AddAsync(unread);
            }
        }


    }
}
