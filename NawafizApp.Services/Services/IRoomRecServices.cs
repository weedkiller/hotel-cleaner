using NawafizApp.Services.Dtos;
using System.Collections.Generic;

namespace NawafizApp.Services.Services
{
    public interface IRoomRecServices
    {
        int Add(roomrecDto roomrecDto);
        List<roomrecDto> getall();
    }
}