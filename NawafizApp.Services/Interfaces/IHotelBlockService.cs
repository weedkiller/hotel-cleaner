using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Interfaces
{
   public interface IHotelBlockService
    {
        int addHotelBlock(HotelBlockDto dto);
        List<HotelBlockDto> GetAll();
        bool delete(int id);
        HotelBlockDto GetById(int id);
        bool edit(HotelBlockDto dto);
    }
}
