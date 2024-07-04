using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Storyteller.Enums;

namespace Storyteller.Models
{
    public class StoryChat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("StoryId")]
        public Story? Story { get; set; }

        public long SerialNumber { get; set; }

        public string? Text { get; set; }

        public string? MediaUrl { get; set; }

        [EnumDataType(typeof(MessageType))]
        public MessageType MessageType { get; set; }

        public string? ReactionType { get; set; }

        public bool ReactionEnabled { get; set; } = false;

        [EnumDataType(typeof(ChatSender))]
        public ChatSender Sender { get; set; }

        public DateTime ChatTimestamp { get; set; }
    }
}
