
namespace LightMessanger.Contracts
{
    public class User : IEntityWithId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        public string Password { get; set; }

        public virtual IList<Group> Groups { get; set; } = new List<Group>();
        public virtual IList<Group> CreatedGroups { get; set; } = new List<Group>();

       
        public virtual IList<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();

    }
}