namespace SignalPlus.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.Data;
    using SignalPlus.DTOs.User;
    using SignalPlus.Models;
    using SignalPlus.Services.Interfaces;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task<MyProfileDto?> GetUserProfileAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Signals)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;

            return new MyProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PasswordHash = user.PasswordHash,
                Signals = user.Signals.ToList()
            };
        }

    }
}