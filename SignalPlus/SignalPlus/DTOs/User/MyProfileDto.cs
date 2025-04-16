namespace SignalPlus.DTOs.User
{
    using System.ComponentModel.DataAnnotations;
    using SignalPlus.Models;

    public class MyProfileDto
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Паролите не съвпадат.")]
        public string? ConfirmPassword { get; set; }

        public int MySignalsCount { get; set; }
    }
}