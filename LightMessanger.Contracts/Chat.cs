
namespace LightMessanger.Contracts
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime LastMessage { get; set; }

        public virtual IList<User> Users { get; set; } = new List<User>(2);
        public virtual IList<Message> Messages { get; set; } = new List<Message>();

    }
}
