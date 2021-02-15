using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Domain.Entities
{
  public  class FIxOrderEqupment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual FixOrder  fixOrder { get; set; }
    }
}
