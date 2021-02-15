using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Interfaces
{
  public  interface IRoomService
    {
        int Add(RoomDto dto);
        bool Edit(RoomDto dto);
        List<Guid> getReservationEmpIdForRoom(int? id);
        Guid getMangerIdForRoom(int? id);
        RoomDto GetById(int id);
      //  List<RoomDto> All(int? hid);
        bool RoomRemove(int roomId);
        List<RoomDto> GetAll();
       // bool EditRoomStat(int? id, int? stId);
    }
}
