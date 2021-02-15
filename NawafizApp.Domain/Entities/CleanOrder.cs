using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Domain.Entities
{
  public  class CleanOrder:IEntityBase
    {
        public int Id { set; get; }

        public Guid? Hoster { set; get; }
        public Guid? moshId { set; get; }
        public Guid? cleaningEmp { set; get; }
        public string Creation_Date { set; get; }
        public string Creation_Time { set; get; }
        public string Creation_At { set; get; }
        public string Description { set; get; }

        public virtual Room Room { set; get; }
        public bool isTaked { get; set; }
        public bool isFinished { set; get; }
        public string startdate { set; get; }
        public string enddate { set; get; }
 
    }
}
