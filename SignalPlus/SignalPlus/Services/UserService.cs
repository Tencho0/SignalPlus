namespace SignalPlus.Services
{
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.Data;
    using SignalPlus.DTOs.User;
    using SignalPlus.Models;
    using SignalPlus.Services.Interfaces;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.PasswordHash != password) // TODO: use hashed passwords
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
                Id = Guid.NewGuid(),
                Name = register.Name,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                PasswordHash = register.Password // TODO: hash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<MyProfileDto?> GetUserProfileAsync(Guid userId)
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