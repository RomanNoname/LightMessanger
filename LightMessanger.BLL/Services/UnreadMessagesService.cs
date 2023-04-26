using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using LightMessanger.DAL.Interfaces;

namespace LightMessanger.BLL.Services
{
    public class UnreadMessagesService : IUnreadMessagesService
    {
        private IUnreadMessagesRepository _context;
        
        public UnreadMessagesService(IUnreadMessagesRepository context)
        {
            _context = context;
        }
        public async Task AddAsync(UnreadMessages item)
        {
           await _context.AddAsync(item);
        }

        public async Task DeleteRangeAsync(IEnumerable<UnreadMessages> item)
        {
           await _context.DeleteRangeAsync(item);
        }

        public async Task DeleteAsync(UnreadMessages item)
        {
            await _context.DeleteAsync(item);
        }

        public async Task<IEnumerable<UnreadMessages>> GetAsync()
        {
          return await _context.GetAllAsync(); 
        }

        public Task<UnreadMessages> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UnreadMessages>> GetUnreadMessagesAsync(int userId, int groupId)
        {
           return await _context.GetByUserIdGroupId(userId, groupId);
        }

        public async Task UpdateAsync(UnreadMessages item)
        {
           await _context.UpdateAsync(item);
        }

       
    }
}
