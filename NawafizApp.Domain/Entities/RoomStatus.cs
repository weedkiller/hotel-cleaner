using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Domain.Entities
{
   public class RoomStatus : IEntityBase
    {
        public int Id { set; get; }
        public string StatusNum { set; get; }
        public int busy { set; get; }
        public int NeedClean { get; set; }

        private ICollection<Room> _rooms;
        public virtual ICollection<Room> Rooms
        {
            get { return _rooms ?? (_rooms = new List<Room>()); }
            set { _rooms = value; }
        }
    }
}
