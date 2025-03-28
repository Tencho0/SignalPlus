namespace SignalPlus.Services
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.Data;
    using SignalPlus.DTOs.Signal;
    using SignalPlus.DTOs.User;
    using SignalPlus.Models;
    using SignalPlus.Services.Interfaces;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> LoginUserAsync(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == email.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, password);
            if (user == null || !isValid)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> RegisterUserAsync(RegisterDTO register)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == register.Email);
            if (exists) return false;

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = register.Email,
                Name = register.Name,
                Email = register.Email,
                NormalizedEmail = register.Email.ToUpper(),
                PhoneNumber = register.PhoneNumber,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, register.Password);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<MyProfileDto?> GetCurrentUserProfileAsync()
        {
            var userContext = _httpContextAccessor.HttpContext?.User;

            if (userContext == null || !userContext.Identity.IsAuthenticated)
                return null;

            var userId = _userManager.GetUserId(userContext);
            var user = await _userManager.Users
                .Include(u => u.Signals)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return new MyProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                MySignalsCount = user.Signals.Count
            };
        }

        public async Task<User?> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity != null && principal.Identity.IsAuthenticated)
            {
                var userIdStr = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userIdStr))
                {
                    return await _context.Users.FindAsync(userIdStr);
                }
            }
            return null;
        }

        public async Task<User> CreateAnonymousUser(NewSignalDTO model)
        {
            var anonUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.SenderName,
                Email = model.SenderEmail,
                PhoneNumber = model.SenderPhone
            };

            await _context.Users.AddAsync(anonUser);
            return anonUser;
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string newPassword)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }


        //public string? GetCurrentUserId()
        //{
        //    var user = _httpContextAccessor.HttpContext?.User;
        //    return _userManager.GetUserId(user); // returns string or null
        //}
    }
}
