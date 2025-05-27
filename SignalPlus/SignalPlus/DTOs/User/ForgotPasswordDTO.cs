namespace SignalPlus.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "Имейлът е задължителен")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес")]
        public string Email { get; set; }
    }
}
