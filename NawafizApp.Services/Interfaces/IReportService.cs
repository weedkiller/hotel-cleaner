using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Interfaces
{
   public interface IReportService
    {
        List<RoomDto> getAllRoomsWithStatusInHotelBlock(int?id);
      // PercentDto getAllRoomsStatusePercentInHotelBlock(int? id);
        List<CleanOrderDto> getAllOrderReport(int? id);
        List<RoomDto> getAllRooms();
    }
}
