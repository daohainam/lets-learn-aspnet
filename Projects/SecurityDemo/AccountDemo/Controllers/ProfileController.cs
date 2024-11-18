using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignInSignOutDemo.Models;
using UserStorage;

namespace SignInSignOutDemo.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userRepository.FindByUserNameAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserProfileModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = user.Roles,
                Password = user.Password
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync(UserProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            if (!IsValidUserName(model.UserName))
            {
                ModelState.AddModelError("UserName", "User name can contain only letters, digits and hypen");
                return View("Index", model);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email ?? "",
                PhoneNumber = model.PhoneNumber ?? "",
                Roles = model.Roles,
                Password = model.Password ?? ""
            };
            await _userRepository.SaveAsync(user);

            return RedirectToAction("Index");
        }
        private static bool IsValidUserName(string userName)
        {
            return userName.All((c) => char.IsAsciiLetterOrDigit(c) || c == '-');
        }
    }
}
