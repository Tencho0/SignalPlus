namespace SignalPlus.Controllers
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.DTOs.User;
    using SignalPlus.Models;
    using SignalPlus.Models.Enums;
    using SignalPlus.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using SignalPlus.Services;
    using Microsoft.AspNetCore.Authorization;

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISignalService _signalService;

        public UserController(IUserService userService, ISignalService signalService)
        {
            _userService = userService;
            _signalService = signalService;
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

            var user = await _userService.LoginUserAsync(login.Email, login.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(login);
            }

            await SignInUser(user);
            // _tokenProvider.SetToken(loginResponseDto.Token);
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            // _tokenProvider.ClearToken();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MyProfile()
        {
            var profile = await _userService.GetCurrentUserProfileAsync();

            if (profile == null) return RedirectToAction("Login");

            return View(profile);
        }

        //TODO: [Authenticcated] -> if user is not logger he is anon user with random Id!
        public async Task<IActionResult> MySignals()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userSignals = await _signalService.GetSignalsByUserIdAsync(userId);
            return View(userSignals); // List<MySignalViewModel>
        }

        public async Task<IActionResult> DeleteProfile()
        {
            var profile = await _userService.GetCurrentUserProfileAsync();

            return View(profile);
        }

        private async Task SignInUser(User model)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id),
                new Claim(ClaimTypes.Name, model.UserName ?? model.Email),
                new Claim(ClaimTypes.Email, model.Email),
            };

            var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);
        }
    }
}
