namespace SignalPlus.DTOs.Signal
{
    using SignalPlus.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class NewSignalDTO
    {
        [Required(ErrorMessage = "Моля, въведете адрес.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Моля, изберете категория.")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Моля, въведете заглавие.")]
        [StringLength(100, ErrorMessage = "Заглавието не може да надвишава 100 символа.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Моля, въведете описание.")]
        [StringLength(1000, ErrorMessage = "Описанието не може да надвишава 1000 символа.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Моля, въведете име.")]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "Моля, въведете телефон.")]
        [Phone(ErrorMessage = "Невалиден телефонен номер.")]
        public string SenderPhone { get; set; }

        [Required(ErrorMessage = "Моля, въведете имейл.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
        public string SenderEmail { get; set; }

        [Required(ErrorMessage = "Трябва да потвърдите, че сте навършили 14 години.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Трябва да потвърдите, че сте навършили 14 години.")]
        public bool IsOver14 { get; set; }

        [Required(ErrorMessage = "Моля, приемете условията за лични данни.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Моля, приемете условията за лични данни.")]
        public bool AcceptsPrivacy { get; set; }

        public bool IsNameVisible { get; set; } = false;

        [Display(Name = "Качи снимки")]
        public List<IFormFile> Images { get; set; } = new();

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }
    }
}
