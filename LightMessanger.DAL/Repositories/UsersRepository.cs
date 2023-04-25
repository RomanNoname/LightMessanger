

using Microsoft.EntityFrameworkCore;

namespace LightMessanger.DAL.Repositories
{
    public class UsersRepository : AbstractRepository<User>, IUsersRepository
    {
       
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
           
        }
        public async Task<User> GetUserByLoginPasswordAsync(string login, string password)
        {
            return (await _context.Set<User>().ToArrayAsync()).SingleOrDefault(e => e.Name == login && e.Password == password);
        }

        public async Task<User> GetValueByСonditionAsync<T>(Func<User, T> valueSelector, T value)
        {
            return ((await _context.Set<User>().ToArrayAsync()).SingleOrDefault(e => valueSelector(e).Equals(value)));
        }

        public async Task<User> GetValueByСonditionAsync<T>(Func<User, T> valueSelector, T value, List<string> includeNodes)
        {
            var context = _context.Users.Include(includeNodes[0]);
            for (int i = 1; i < includeNodes.Count; i++)
            {
                context = context.Include(includeNodes[i]);
            }
            return (await context.ToListAsync()).SingleOrDefault(e => valueSelector(e).Equals(value));
        }
    }
}
