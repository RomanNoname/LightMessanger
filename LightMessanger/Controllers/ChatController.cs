using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LightMessanger.WEB.Controllers
{
    public class ChatController : Controller
    {
        private IUsersService _usersService;
        private IGroupsService _groupsService;
        public ChatController(IUsersService usersService, IGroupsService groupsService)
        {
            _usersService = usersService;
            _groupsService = groupsService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string name)
        {
            var user = await _usersService.GetValueByСonditionAsync(e => e.Name, User.Identity.Name);
            var group = new Group() { Name = name, UserGenerated = user };
            await _groupsService.AddAsync(group);
            await _groupsService.AddUserInGroup(user, group.Id);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        [Route("chat/{name}")]
        public async Task<IActionResult> Chat(string name)
        {
            Response.Cookies.Append("chat", name);
            var model = await _groupsService.GetGroupWithUsers(name);
            return View(model);
        }
        [HttpGet]
        [Authorize]
        [Route("chats")]
        public async Task<IActionResult> Chats()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUser(int id)
        {
            var user = await _usersService.GetValueByСonditionAsync(u => u.Name, User.Identity.Name);
            if (user != null)
            {
                await _groupsService.AddUserInGroup(user, id);
                return RedirectToAction("Chat", "Chat", new { name = (await _groupsService.GetByIdAsync(id)).Name });
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
