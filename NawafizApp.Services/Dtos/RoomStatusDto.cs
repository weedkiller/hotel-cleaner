using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos
{
  public  class RoomStatusDto
    {
        public int Id { set; get; }
        public string StatusNum { set; get; }
        public string Status { set; get; }
        public string RoomNum { set; get; }
        public int? Room_Id { set; get; }
    }
}
