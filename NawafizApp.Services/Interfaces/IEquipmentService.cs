using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Interfaces
{
   public interface IEquipmentService
    {
        int Add(EquipmentDto dto);
        bool Edit(EquipmentDto dto);

        EquipmentDto GetById(int id);
        List<EquipmentDto> All(int rid);
        bool EquipmentRemove(int Id);
        void checkedToggle(int id);
    }
}
