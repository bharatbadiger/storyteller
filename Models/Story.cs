using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storyteller.Models
{
    public class Story
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public string? UserMe { get; set; }

        public string? UserOther { get; set; }

        public string? Tags { get; set; }
    }
}
