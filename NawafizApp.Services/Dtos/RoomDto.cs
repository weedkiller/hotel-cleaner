using NawafizApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos
{
    public class RoomDto
    { 
            public int Id { set; get; }
            public string RoomNum { set; get; }

            public string RoomDirection { set; get; }
            public  int HotelBlock_id { set; get; }
            public string blockName { get; set; }
            public  int RoomType_id { set; get; }
            public string TypeName { get; set; }
            public bool Isrequisted { get; set; }
        public bool Isrequistedfix { get; set; }
        public bool Isbusy { get; set; }
            public bool IsNeedfix { get; set; }
            public bool isneedclean { get; set; }
            public bool IsInService { get; set; }

          



        }
    }



