using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storyteller.Models
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public double Amount { get; set; }

        public long Duration { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Logo { get; set; }

        public string? OtherInfo { get; set; }
    }
}
