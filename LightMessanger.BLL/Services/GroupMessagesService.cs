using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using LightMessanger.DAL.Interfaces;


namespace LightMessanger.BLL.Services
{
    public class GroupMessagesService : IGroupMessagesService
    {
        private IGroupMessageRepository _context;
        private IGroupsService _groupsService;

        public GroupMessagesService(IGroupMessageRepository context, IGroupsService groupsService)
        {
            _context = context;
            _groupsService = groupsService;
        }
        public async Task AddAsync(GroupMessage item)
        {
            if (string.IsNullOrEmpty(item.Content) || item.Content.Length < 1 || item.Content.Length > 500)
                throw new ArgumentException("Invalid Content");
            var group = await _groupsService.GetByIdAsync(item.ChatGroupId);
            if (group is null)
                throw new ArgumentNullException("Group not exist");

            await _context.AddAsync(item);
            
            group.LastMessage = item.Created;
            await _groupsService.UpdateAsync(group);
        }

        public async Task DeleteAsync(GroupMessage item)
        {
            if (item is null)
                throw new InvalidOperationException("Message not exist");
            await _context.DeleteAsync(item);
        }

        public async Task<IEnumerable<GroupMessage>> GetAsync()
        {
            return await _context.GetAllAsync();
        }

        public async Task<GroupMessage> GetByIdAsync(int id)
        {
            return await _context.GetByIdAsync(id);
        }

        public async Task<IEnumerable<GroupMessage>> GetGroupMessagesByGroupNameAsync(string name)
        {
            return await _context.GetMessagesByGroupNameAsync(name);
        }

        public async Task UpdateAsync(GroupMessage item)
        {
            if (string.IsNullOrEmpty(item.Content) || item.Content.Length < 1 || item.Content.Length > 500)
                throw new ArgumentException("Invalid Content");
            var group = await _groupsService.GetByIdAsync(item.ChatGroupId);
            if (group is null)
                throw new ArgumentNullException("Group not exist");
            await _context.UpdateAsync(item);
        }
    }
}
