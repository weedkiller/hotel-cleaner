using NawafizApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos
{
   public class EquipmentDto
    {
        public int Id { set; get; }

        public string Name { set; get; }
        public string Description { set; get; }
        public string Stauts { set; get; }
        public bool needfix { set; get; }
        public bool ishere { set; get; }
        public  int Room_id { set; get; }

    }
}
