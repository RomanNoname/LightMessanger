using LightMessanger.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightMessanger.BLL.Interfaces
{
    public interface IUnreadMessagesService:IService<UnreadMessages>
    {
        public Task<IEnumerable<UnreadMessages>> GetUnreadMessagesAsync(int userId, int groupId);
        public Task DeleteRangeAsync(IEnumerable<UnreadMessages> item);
    }
}
