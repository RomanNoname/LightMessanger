
using LightMessanger.Contracts;

namespace LightMessanger.BLL.Interfaces
{
    public interface IGroupsService:IService<LightMessanger.Contracts.Group>
    {
        public Task AddUserInGroup(User user, int IdGroup);
        public Task<Group> GetValueByСonditionAsync<T>(Func<Group, T> valueSelector, T value);
        public Task<IEnumerable<Group>> SearchBySubstringInNameAsync(string value);

        public Task<Group> GetGroupWithUsers(string name);
    }
}
