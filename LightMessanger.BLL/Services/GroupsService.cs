using LightMessanger.BLL.Interfaces;
using LightMessanger.Contracts;
using LightMessanger.DAL.Interfaces;

namespace LightMessanger.BLL.Services
{
    public class GroupsService : IGroupsService
    {
        private IGroupRepository _context;
        private string _folder;
        public GroupsService(IGroupRepository context, string folder = "wwwroot")
        {
            _context = context;
        }
        public async Task AddAsync(Group item)
        {
            if (item.Name.Length < 3 || item.Name.Length > 30)
                throw new ArgumentException("Invalid name");
            if (await GetValueByСonditionAsync(u => u.Name, item.Name) != null)
                throw new ArgumentException("GroupName exist");
                await _context.AddAsync(item);
        }
        public async Task<IEnumerable<Group>> SearchBySubstringInNameAsync(string value)
        {
            return await _context.SearchBySubstringInNameAsync(value);
        }
        public async Task<Group> GetValueByСonditionAsync<T>(Func<Group, T> valueSelector, T value)
        {
            return await _context.GetValueByСonditionAsync(valueSelector, value);
        }

        public async Task AddUserInGroup(User user, string groupName)
        {
            var group = await GetGroupWithUsers(groupName);
            if (group is null)
                throw new ArgumentNullException("Group not found");
            if (group.Users.Any(x => x.Id.Equals(user.Id)))
                throw new ArgumentException("User already exist in group");
          

            group.Users.Add(user);
            await _context.UpdateAsync(group);
        }

        public async Task DeleteAsync(Group item)
        {
            if (item is null)
                throw new ArgumentNullException("Group not exist");
            await _context.DeleteAsync(item);
        }

        public async Task<IEnumerable<Group>> GetAsync()
        {
            return await _context.GetAllAsync();
        }

        public Task<Group> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Group item)
        {
            if (item.Name.Length < 3 || item.Name.Length > 30)
                throw new ArgumentException("Invalid name");
           
            await _context.UpdateAsync(item);
        }

        public async Task<Group> GetGroupWithUsers(string name)
        {
            return await _context.GetValueByСonditionAsync(u => u.Name, name, new List<string>() {
            "Users",
            "GroupMessages"
            });
        }
    }
}
