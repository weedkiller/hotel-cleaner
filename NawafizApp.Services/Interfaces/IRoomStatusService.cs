using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Interfaces
{
  public  interface IRoomStatusService
    {
        int Add(RoomStatusDto dto);
        bool Edit(RoomStatusDto dto);

        RoomStatusDto GetById(int id);
        List<RoomStatusDto> All(int? rid);
        bool RoomStatusRemove(int Id);
        List<RoomStatusDto> getall();
    }
}
