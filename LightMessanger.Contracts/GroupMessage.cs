
namespace LightMessanger.Contracts
{
    public class GroupMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public string Content { get; set; }

        [ForeignKey("sender_user_id")]
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("group_id")]
        [Required]
        public int ChatGroupId { get; set; }
        public virtual Group ChatGroup { get; set; }
    }
}
