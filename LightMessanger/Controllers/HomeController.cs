using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using LightMessanger.Models;
using LightMessanger.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LightMessanger.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUsersService _usersService;
        private IGroupsService _groupsService;
        private IGroupMessagesService _groupMessagesService;
        private IUnreadMessagesService _unreadMessagesService;
        public HomeController(ILogger<HomeController> logger, IUsersService usersService, IGroupsService groupsService, IGroupMessagesService groupMessagesService, IUnreadMessagesService unreadMessagesService)
        {

            _logger = logger;
            _groupsService = groupsService;
            _usersService = usersService;
            _groupMessagesService = groupMessagesService;
            _unreadMessagesService = unreadMessagesService;
        }


        public async Task<IActionResult> Index(string search, string currentChat)
        {
            GroupsChat model = new GroupsChat();
            ViewBag.Search = search;
            if (currentChat != null)
            {
                if (!(await _groupsService.GetGroupWithUsers(currentChat)).Users.Any(u => u.Name.Equals(User.Identity.Name)))
                {
                    var objectModel = await _groupsService.GetGroupWithUsers(currentChat);
                    return View("JoinToChat", objectModel);
                }
                ViewBag.Chat = currentChat;
                Response.Cookies.Append("chat", currentChat);
            }
            if (string.IsNullOrWhiteSpace(search))
                model.Groups = (await _usersService.GetAllIncludes(e => e.Name, User.Identity.Name)).Groups;
            else
                model.Groups = await _groupsService.SearchBySubstringInNameAsync(search);


            var user = await _usersService.GetValueByСonditionAsync(u => u.Name, User.Identity.Name);
            var group = await _groupsService.GetValueByСonditionAsync(u => u.Name, currentChat);

            if (group != null)
            {
                var read = await _unreadMessagesService.GetUnreadMessagesAsync(user.Id, group.Id);
                if(read != null)    
                await _unreadMessagesService.DeleteRangeAsync(read);
            }

            ///
           
            var unread = await _unreadMessagesService.GetAsync();
             model.Unread= unread.Where(m=>m.UserId == user.Id).Select(x=>x.GroupId);
            ///

            model.Message = await _groupMessagesService.GetGroupMessagesByGroupNameAsync(currentChat);
            return View(model);

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUser(int id)
        {
            var user = await _usersService.GetValueByСonditionAsync(u => u.Name, User.Identity.Name);
            if (user != null)
            {
                await _groupsService.AddUserInGroup(user, id);
                return RedirectToAction("Index", "Home", new { currentChat = (await _groupsService.GetByIdAsync(id)).Name });
            }
            return RedirectToAction("Login", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string name)
        {
            var user = await _usersService.GetValueByСonditionAsync(e => e.Name, User.Identity.Name);
            var group = new Group() { Name = name, UserGenerated = user };
            await _groupsService.AddAsync(group);
            await _groupsService.AddUserInGroup(user, group.Id);
            return RedirectToAction("Index", "Home",new {currentChat = name});
        }
    }
}