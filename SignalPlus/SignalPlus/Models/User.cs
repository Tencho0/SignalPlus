namespace SignalPlus.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        // Navigation property - list of signals submitted by the user
        public ICollection<Signal> Signals { get; set; } = new List<Signal>();
    }

}
