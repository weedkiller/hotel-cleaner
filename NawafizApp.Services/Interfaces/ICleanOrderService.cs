using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Interfaces
{
   public interface ICleanOrderService
    {
        Guid getmoshbyroomId(int rid);
        int addOrder(CleanOrderDto dto);
        List<CleanOrderDto> GetAll();
        bool delete(int id);
        CleanOrderDto GetById(int id);
        bool edit(CleanOrderDto dto);
        bool setIsSeenTrue();
        bool setIsSeenTrueForMosherf();
    }
}
