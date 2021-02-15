using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Domain.Entities
{
   public class Equipment : IEntityBase
    {
        public int Id { set; get; }

        public string Name { set; get; }
        public string Description { set; get; }
        public string Stauts { set; get; }
        public bool needfix { set; get; }
        public bool ishere { set; get; }
        public virtual Room Room { set; get; }


    }
}
