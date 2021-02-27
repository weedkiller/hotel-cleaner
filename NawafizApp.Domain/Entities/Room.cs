using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Domain.Entities
{
 public  class Room : IEntityBase
    {
        public int Id { set; get; }
        public string RoomNum { set; get; }
        public bool Isrequisted { get; set; }
        public bool IsFixrequisted { get; set; }
        public bool Isrequistedfix { get; set; }

        public string RoomDirection { set; get; }
        public virtual HotelBlock HotelBlock { set; get; }
        public virtual RoomType RoomType { set; get; }
        public bool Isbusy { get; set; }
        public bool IsNeedfix { get; set; }
        public bool isneedclean { get; set; }
        public bool IsInService { get; set; }

        private ICollection<Equipment> _equipment;
        public virtual ICollection<Equipment> Equipments
        {
            get { return _equipment ?? (_equipment = new List<Equipment>()); }
            set { _equipment = value; }
        }
        private ICollection<CleanOrder> _CleanOrder;
        public virtual ICollection<CleanOrder> Orders
        {
            get { return _CleanOrder ?? (_CleanOrder = new List<CleanOrder>()); }
            set { _CleanOrder = value; }
        }
        private ICollection<FixOrder> _fixOrder;
        public virtual ICollection<FixOrder> FixOrder
        {
            get { return _fixOrder ?? (_fixOrder = new List<FixOrder>()); }
            set { _fixOrder = value; }
        }
        private ICollection<RoomRec> _RoomRec;
        public virtual ICollection<RoomRec> RoomRec
        {
            get { return _RoomRec ?? (_RoomRec = new List<RoomRec>()); }
            set { _RoomRec = value; }
        }


    }
}
