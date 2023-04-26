using LightMessanger.Contracts;

namespace LightMessanger.WEB.Models
{
    public class GroupsChat
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<GroupMessage> Message { get; set; }

        public IEnumerable<int> Unread { get; set; }

    }
}
