using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightMessanger.DAL.Interfaces
{
    public interface IUnreadMessagesRepository:IRepository<UnreadMessages>
    {
        public Task<IEnumerable<UnreadMessages>> GetByUserIdGroupId(int userId, int groupId);
        public Task DeleteRangeAsync(IEnumerable<UnreadMessages> unreadMessages);
    }
}
