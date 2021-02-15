using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Domain.Entities
{
  public  class Notifictation:IEntityBase
    {
        public int Id { set; get; }
        public Guid senderId { set; get; }
        public virtual User User { set; get; }
        public Guid RevieverId { set; get; }
        public string NotText { set; get; }
        public string NotDateTime { set; get; }
        public bool IsRead { set; get; }
        public int? Room_ID { get; set; }
        public int? Equipment_ID { get; set; }
    }
}
