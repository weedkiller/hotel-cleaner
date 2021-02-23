using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Domain.Entities
{
   public class RoomRec
    {

        public int Id { get; set; }
        public Room Room { get; set; }

        public string Recoed { get; set; }
        public string Datetime { get; set; }

    }
}
