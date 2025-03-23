namespace SignalPlus.DTOs.User
{
    using SignalPlus.Models;

    public class MyProfileDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<Signal> Signals { get; set; }
    }
}
