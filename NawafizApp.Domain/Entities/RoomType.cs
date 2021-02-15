using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Domain.Entities
{
   public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }


        private ICollection<Room> _Room;
        public virtual ICollection<Room> Rooms
        {
            get { return _Room ?? ( _Room = new List<Room>()); }
            set { _Room = value; }
        }
    }
}
