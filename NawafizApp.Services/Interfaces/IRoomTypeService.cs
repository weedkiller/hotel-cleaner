using NawafizApp.Domain.Entities;
using NawafizApp.Services.Dtos;
using System.Collections.Generic;

namespace NawafizApp.Services.Interfaces
{
   public interface IRoomTypeService
    {
        int Add(RoomTypeDto roomTypeDto);
        bool Edit(RoomTypeDto roomTypeDto);
        List<RoomTypeDto> GetRoomTypes();
        bool delete(int id);
        RoomTypeDto getbyidGetById(int id);

    }
}