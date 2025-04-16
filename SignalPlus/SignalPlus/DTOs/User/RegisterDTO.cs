namespace SignalPlus.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
