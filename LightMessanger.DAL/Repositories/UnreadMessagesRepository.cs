using Microsoft.EntityFrameworkCore;

namespace LightMessanger.DAL.Repositories
{
    public class UnreadMessagesRepository : AbstractRepository<UnreadMessages>, IUnreadMessagesRepository
    {
        public UnreadMessagesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task AddAsync(UnreadMessages item)
        {
            await _context.UnreadMessages.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<UnreadMessages> item)
        {
            _context.UnreadMessages.RemoveRange(item);
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<UnreadMessages>> GetAllAsync()
        {
            return await _context.UnreadMessages.ToListAsync();
        }

        public override async Task<UnreadMessages> GetByIdAsync(int id)
        {
           return await _context.UnreadMessages.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<UnreadMessages>> GetByUserIdGroupId(int userId, int groupId)
        {
            return await _context.UnreadMessages.Where(m=>m.UserId.Equals(userId)&&m.GroupId.Equals(groupId)).ToListAsync();
        }

        public async Task UpdateAsync(UnreadMessages item)
        {
             _context.UnreadMessages.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
