using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Domain.Entities
{
    public class HotelBlock : IEntityBase
    {
        public int Id { set; get; }
        public string BlockNum {set; get;}
        public string BlockName { set; get; }
        public string BlockDescription { set; get; }
        public string NmberOfTheFloorsIntheBlock { set; get; }
        public string NmberOfTheRoomsIntheBlock { set; get; }
        private  ICollection<User> _user;
        public virtual ICollection<User> Users
        {
            get { return _user ?? (_user = new List<User>()); }
            set { _user = value; }
        }
        private ICollection<Room> _rooms;
        public virtual ICollection<Room> Rooms
        {
            get { return _rooms ?? (_rooms = new List<Room>()); }
            set { _rooms = value; }
        }
    }
}
