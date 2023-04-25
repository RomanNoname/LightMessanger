
namespace LightMessanger.Contracts
{
    public class Message : IEntityWithId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public string Content { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        [ForeignKey("sender_user_id")]
        [Required]
        public int SenderUserId { get; set; }
        public virtual User Sender { get; set; }

        [ForeignKey("reciver_user_id")]
        [Required]
        public int RecieverUserId { get; set; }
        public virtual User Reciever { get; set; }
    }
}
