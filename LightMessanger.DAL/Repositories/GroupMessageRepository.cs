using Microsoft.EntityFrameworkCore;

namespace LightMessanger.DAL.Repositories
{
    public class GroupMessageRepository : AbstractRepository<GroupMessage>, IGroupMessageRepository
    {
        public GroupMessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GroupMessage>> GetMessagesByGroupNameAsync(string name)
        {
            return await _context.GroupMessages.Include(x => x.ChatGroup).Include(x => x.User).Where(x => x.ChatGroup.Name.Equals(name)).ToListAsync();
        }
    }
}
