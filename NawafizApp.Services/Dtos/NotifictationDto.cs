using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos
{
   public class NotifictationDto
    {
        public int Id { set; get; }
        public Guid? senderId { set; get; }
        public String SenderName { set; get; }
        public Guid? RevieverId { set; get; }
        public string RevieverName { set; get; }
        public string NotText { set; get; }
        public string NotDateTime { set; get; }
        public bool IsRead { set; get; }
        public int? Room_ID { get; set; }
        public string RoomName { set; get; }
        public int? Equipment_ID { get; set; }
        public string orderId { set; get; }
        public string EquipmentName { get; set; }
    }
}
