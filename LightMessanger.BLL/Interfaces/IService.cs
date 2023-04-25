using LightMessanger.Contracts;

namespace LightMessanger.BLL.Interfaces
{

    public interface IService<T> where T : class, IEntityWithId
    {
        public Task AddAsync(T item);
        public Task UpdateAsync(T item);
        public Task DeleteAsync(T item);
        public Task<IEnumerable<T>> GetAsync();
        public Task<T> GetByIdAsync(int id);
    }

}
