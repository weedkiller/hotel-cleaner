using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Services
{
   public interface INotifictationService
    {
        int Add(NotifictationDto dto);
        bool Edit(NotifictationDto dto);

        NotifictationDto GetById(int id);
        List<NotifictationDto> All(Guid? userid);
        bool NotifictationRemove(int Id);
    }
}
