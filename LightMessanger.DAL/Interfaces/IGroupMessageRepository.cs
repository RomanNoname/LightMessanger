
namespace LightMessanger.DAL.Interfaces
{
    public interface IGroupMessageRepository:IRepository<GroupMessage>
    {
        public Task<IEnumerable<GroupMessage>> GetMessagesByGroupNameAsync(string name);
    }
}
