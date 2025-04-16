namespace SignalPlus.Services.Interfaces
{
    public interface IReCaptchaService
    {
        Task<bool> VerifyToken(string token);
    }
}
