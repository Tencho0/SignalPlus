namespace SignalPlus.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public class SignalImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public int SignalId { get; set; }

        [ForeignKey(nameof(SignalId))]
        public virtual Signal Signal { get; set; }
    }
}
