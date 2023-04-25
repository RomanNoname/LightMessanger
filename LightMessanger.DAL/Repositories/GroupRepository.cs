using Microsoft.EntityFrameworkCore;

namespace LightMessanger.DAL.Repositories
{
    public class GroupRepository : AbstractRepository<Group>, IGroupRepository
    {

        public GroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Group> GetValueByСonditionAsync<T>(Func<Group, T> valueSelector, T value)
        {
            return (await _context.Set<Group>().Include("GroupMessages.User").ToArrayAsync()).SingleOrDefault(e => valueSelector(e).Equals(value));
        }

        public async Task<Group> GetValueByСonditionAsync<T>(Func<Group, T> valueSelector, T value, List<string> includeNodes)
        {
            var context = _context.Groups.Include(includeNodes[0]);
            for (int i = 1; i < includeNodes.Count; i++)
            {
                context = context.Include(includeNodes[i]);
            }
            return (await context.ToListAsync()).SingleOrDefault(e => valueSelector(e).Equals(value));
        }

        public async Task<IEnumerable<Group>> SearchBySubstringInNameAsync(string value)
        {
            return await _context.Set<Group>().Include("GroupMessages.User").Where(x => x.Name.ToLower().Contains(value.ToLower())).ToListAsync();
        }
    }
}
