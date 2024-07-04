using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storyteller.Models
{
    public class UserFollowAuthor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }
    }
}
