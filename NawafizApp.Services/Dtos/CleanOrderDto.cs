using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos
{
   public class CleanOrderDto
    {
        public int Id { set; get; }
        public Guid? Hoster { set; get; }
        public string HosterName { get; set; }
        public Guid? moshId { set; get; }
        public string    moshrefname { set; get; }
        public Guid? cleaningEmp { set; get; }
        public string   empName { get; set; }

        public string Creation_Date { set; get; }
        public string Creation_Time { set; get; }
        public string Creation_At { set; get; }
        public string Description { set; get; }


        public int? Room_ID { get; set; }
        public string Roomnu { get; set; }
        public bool Istaked { get; set; }
        public bool isFinished { set; get; }
        public string startdate { set; get; }
        public string enddate { set; get; }
    }
}
