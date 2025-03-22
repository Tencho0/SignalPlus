namespace SignalPlus.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        // Optional for non-registered users
        public string? PasswordHash { get; set; }

        // Navigation property - list of signals submitted by the user
        public ICollection<Signal> Signals { get; set; }
    }

}
