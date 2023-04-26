using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LightMessanger.WEB.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : Controller
    {
        private IUsersService _usersService;
        private IGroupsService _groupsService;
        private IUnreadMessagesService _unreadMessagesService;
        public ChatController(IUsersService usersService, IGroupsService groupsService, IUnreadMessagesService unreadMessagesService)
        {
            _usersService = usersService;
            _groupsService = groupsService;
            _unreadMessagesService = unreadMessagesService;
        }
        [HttpPost]
        [Route("AddUnreadMessage")]
        public async Task<IActionResult> AddUnreadMessage(string groupName)
        {
            var user = await _usersService.GetUserGroupsAsync(u => u.Name, User.Identity.Name);
            var group = user.Groups.FirstOrDefault(g => g.Name == groupName);
            var unread = new UnreadMessages()
            {
                User = user,
                Group = group
            };
            await _unreadMessagesService.AddAsync(unread);
            return Ok();
        }
    }
}
