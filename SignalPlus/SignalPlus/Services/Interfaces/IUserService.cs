namespace SignalPlus.Services.Interfaces
{
    using SignalPlus.DTOs.User;
    using SignalPlus.Models;

    public interface IUserService
    {
        Task<User?> LoginUserAsync(string email, string password);

        Task<bool> RegisterUserAsync(RegisterDTO register);

        Task<MyProfileDto?> GetCurrentUserProfileAsync();
    }
}
