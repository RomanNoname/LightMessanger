
namespace LightMessanger.Contracts
{
    public class Group : IEntityWithId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public DateTime LastMessage { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        public int UserGeneratedId { get; set; }

        [ForeignKey("UserGeneratedId")]
        public virtual User UserGenerated { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();
    }
}
