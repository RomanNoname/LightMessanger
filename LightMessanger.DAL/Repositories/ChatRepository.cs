namespace LightMessanger.DAL.Repositories
{
    public class ChatRepository : AbstractRepository<Group>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
