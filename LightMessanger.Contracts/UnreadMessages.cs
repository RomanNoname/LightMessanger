using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightMessanger.Contracts
{
    public class UnreadMessages:IEntityWithId
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}
