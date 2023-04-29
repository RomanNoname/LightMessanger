using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using LightMessanger.DAL.Interfaces;

namespace LightMessanger.BLL.Services
{
    public class UnreadMessagesService : IUnreadMessagesService
    {
        private IUnreadMessagesRepository _context;
        private IGroupsService _groupsService;
        
        public UnreadMessagesService(IUnreadMessagesRepository context, IGroupsService groupsService)
        {
            _groupsService = groupsService;
            _context = context;
        }
        public async Task AddAsync(UnreadMessages item)
        {
           var group =  await _groupsService.GetGroupWithUsers(item.Group.Name);
            if (group == null)
                throw new ArgumentNullException("Group not exist");
            if (!group.Users.Contains(item.User))
                throw new ArgumentException("Group doesn't contain user");
           await _context.AddAsync(item);
        }

        public async Task DeleteRangeAsync(IEnumerable<UnreadMessages> item)
        {
            if(item == null)
                throw new ArgumentNullException("UnreadMessages is null");
           await _context.DeleteRangeAsync(item);
        }

        public async Task DeleteAsync(UnreadMessages item)
        {
            if (item == null)
                throw new ArgumentNullException("UnreadMessage is null");
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
            var group = await _groupsService.GetGroupWithUsers(item.Group.Name);
            if (group == null)
                throw new ArgumentNullException("Group not exist");
            if (group.Users.Contains(item.User))
                throw new ArgumentException("Group doesn't contain user");
            await _context.UpdateAsync(item);
        }

       
    }
}
