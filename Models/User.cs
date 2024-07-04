using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Storyteller.Enums;

namespace Storyteller.Models
{
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Name { get; set; }

        [Column(TypeName = "varchar(10)")]
        [StringLength(10)]
        public string? Mobile { get; set; }

        [Column(TypeName = "varchar(100)")]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Image { get; set; }

        [EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; } = UserStatus.ACTIVE;

        public DateTime? SubscribedOn { get; set; }

        public DateTime? SubscriptionEndsOn { get; set; }

        [EnumDataType(typeof(SubscriptionStatus))]
        public SubscriptionStatus SubscribtionStatus { get; set; } = SubscriptionStatus.NOT_SUBSCRIBED;

        [ForeignKey("SubscriptionId")]
        public Subscription? Subscription { get; set; }

        public string? Bio { get; set; }

        public string? FirebaseAuthId { get; set; }
    }
}
