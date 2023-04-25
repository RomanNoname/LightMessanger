

namespace LightMessanger.DAL.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        public Task<User> GetUserByLoginPasswordAsync(string login, string password);
        public Task<User> GetValueByСonditionAsync<T>(Func<User, T> valueSelector, T value);

        public Task<User> GetValueByСonditionAsync<T>(Func<User, T> valueSelector, T value, List<string> includeNodes);
    }
}
