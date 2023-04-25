

using LightMessanger.Contracts;

namespace LightMessanger.BLL.Interfaces
{
    public interface IUsersService : IService<User>
    {
        public Task<User> GetUserByLoginPasswordAsync(string login, string password);
        public Task<User> GetValueByСonditionAsync<T>(Func<User, T> valueSelector, T value);
        public Task<User> GetAllIncludes<T>(Func<User, T> valueSelector, T value);

        public Task<User> GetUserGroupsAsync<T>(Func<User, T> valueSelector, T value);
    }
}
