using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SignInSignOutDemo.Models;
using System.Security.Claims;
using UserStorage;

namespace SignInSignOutDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOnAsync(LogOnModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userRepository.FindByUserNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.UserName),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!IsValidUserName(model.UserName))
            {
                ModelState.AddModelError("UserName", "User name can contain only letters, digits and hypen");
                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email ?? "",
                PhoneNumber = model.PhoneNumber ?? "",
                Password = model.Password ?? "",
                Roles = ["User"]
            };

            await _userRepository.SaveAsync(user);
            return RedirectToAction("LogOn");
        }

        private static bool IsValidUserName(string userName)
        {
            return userName.All((c) => char.IsAsciiLetterOrDigit(c) || c == '-');
        }

    }
}
