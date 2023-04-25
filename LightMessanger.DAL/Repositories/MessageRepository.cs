namespace LightMessanger.DAL.Repositories
{
    public class MessageRepository : AbstractRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
