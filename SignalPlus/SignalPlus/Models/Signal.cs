namespace SignalPlus.Models
{
    using System.ComponentModel.DataAnnotations;
    using SignalPlus.Models.Enums;

    public class Signal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public Status Status { get; set; } = Status.Регистриран;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string LocationAddress { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
