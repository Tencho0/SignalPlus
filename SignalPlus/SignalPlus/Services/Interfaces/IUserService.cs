namespace SignalPlus.Services.Interfaces
{
    using System.Security.Claims;
    using SignalPlus.DTOs.Signal;
    using SignalPlus.DTOs.User;
    using SignalPlus.Models;

    public interface IUserService
    {
        Task<User?> LoginUserAsync(string email, string password);

        Task<bool> RegisterUserAsync(RegisterDTO register);

        Task<MyProfileDto?> GetCurrentUserProfileAsync();

        Task<User?> GetCurrentUserAsync(ClaimsPrincipal principal);

        Task<User> CreateAnonymousUser(NewSignalDTO model);
    }
}
