using System;
using System.Collections.Generic;

namespace NawafizApp.Domain.Entities
{
    public class FixOrder
    {
        public int Id { set; get; }

        public Guid? Hoster { set; get; }
        public Guid? moshId { set; get; }
        public Guid? maitremp { set; get; }
        public string Creation_Date { set; get; }
        public string Creation_Time { set; get; }
        public string Creation_At { set; get; }
        public string Description { set; get; }

        public virtual Room Room { set; get; }
        public bool isTaked { get; set; }
        public bool isFinished { set; get; }
        public string startdate { set; get; }
        public string enddate { set; get; }

        private ICollection<FIxOrderEqupment> _FIxOrderEqupment;
        public virtual ICollection<FIxOrderEqupment> FIxOrderEqupment
        {
            get { return _FIxOrderEqupment ?? (_FIxOrderEqupment = new List<FIxOrderEqupment>()); }
            set { _FIxOrderEqupment = value; }
        }


    }
}