namespace SignalPlus.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.DTOs.User;
    using SignalPlus.Models;
    using SignalPlus.Models.Enums;
    using SignalPlus.Services.Interfaces;

    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (!ModelState.IsValid) return View(login);

            var user = await _userService.AuthenticateAsync(login.Email, login.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(login);
            }

            // TEMP login via session
            //HttpContext.Session.SetString("UserId", user.Id.ToString());
            //HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToAction("Index", "Home");
        }

        // GET: /User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /User/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (!ModelState.IsValid) return View(register);

            var success = await _userService.RegisterUserAsync(register);
            if (!success)
            {
                ModelState.AddModelError("Email", "Email is already registered");
                return View(register);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MyProfile()
        {
            //var userIdStr = HttpContext.Session.GetString("UserId");
            //if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login");

            //var userId = Guid.Parse(userIdStr);
            var userId = Guid.Parse("73DCB058-FC94-4C80-9745-1E7C0543EC56");
            var profile = await _userService.GetUserProfileAsync(userId);

            if (profile == null) return RedirectToAction("Login");

            return View(profile);
        }

        public async Task<IActionResult> DeleteProfile()
        {
            var userId = Guid.Parse("73DCB058-FC94-4C80-9745-1E7C0543EC56");
            var profile = await _userService.GetUserProfileAsync(userId);

            return View(profile);
        }
    }
}
