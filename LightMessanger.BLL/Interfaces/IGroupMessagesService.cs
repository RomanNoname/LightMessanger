using LightMessanger.Contracts;

namespace LightMessanger.BLL.Interfaces
{
    public interface IGroupMessagesService : IService<GroupMessage>
    {
        public Task<IEnumerable<GroupMessage>> GetGroupMessagesByGroupNameAsync(string name);
    }
}
