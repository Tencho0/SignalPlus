namespace SignalPlus.Services
{
    using Microsoft.Extensions.Options;
    using SignalPlus.Configuration;
    using SignalPlus.Services.Interfaces;
    using System.Text.Json;

    public class ReCaptchaService : IReCaptchaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GoogleReCaptchaConfig _config;

        public ReCaptchaService(IHttpClientFactory httpClientFactory, IOptions<GoogleReCaptchaConfig> config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config.Value;
        }

        public async Task<bool> VerifyToken(string? token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={_config.SecretKey}&response={token}",
                null);

            var json = await response.Content.ReadAsStringAsync();
            JsonElement result = JsonSerializer.Deserialize<JsonElement>(json);

            return result.TryGetProperty("success", out var success) && success.GetBoolean();
        }
    }
}
