using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using LightMessanger.WEB.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LightMessanger.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUsersService _usersService;
        public AccountController(IUsersService userRepository)
        {
            _usersService = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (await _usersService.GetValueByСonditionAsync(u => u.Name, model.UserName) != null)
                ModelState.AddModelError(nameof(model.UserName), "This name already exist");
            if (await _usersService.GetValueByСonditionAsync(u => u.Email, model.Email) != null)
                ModelState.AddModelError(nameof(model.Email), "This mail has already been used");
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Name = model.UserName,
                    Email = model.Email,
                    Password = model.Password
                };
                await _usersService.AddAsync(user);

                return await Login(new LoginModel() { Password = model.Password, UserName = model.UserName });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpGet]

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginModel model)
        {
            if (await _usersService.GetValueByСonditionAsync(u => u.Name, model.UserName) == null)
            {
                ModelState.AddModelError(nameof(model.UserName), "This user not exist");
                return View(model);
            }
            if (await _usersService.GetUserByLoginPasswordAsync(model.UserName, model.Password) == null)
                ModelState.AddModelError(nameof(model.Password), "Wrong password");

            if (ModelState.IsValid)
            {
                await Authenticate(model);
                return RedirectToAction("Index", "Home");
            }
            else
                return View(model);


        }
        private async Task Authenticate(LoginModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, model.UserName),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
