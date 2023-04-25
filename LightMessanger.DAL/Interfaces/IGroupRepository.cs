

namespace LightMessanger.DAL.Interfaces
{
    public interface IGroupRepository:IRepository<Group>
    {
        public Task<Group> GetValueByСonditionAsync<T>(Func<Group, T> valueSelector, T value);
        public Task<Group> GetValueByСonditionAsync<T>(Func<Group, T> valueSelector, T value, List<string> nodes);
        public Task<IEnumerable<Group>> SearchBySubstringInNameAsync(string value);
    }
}
