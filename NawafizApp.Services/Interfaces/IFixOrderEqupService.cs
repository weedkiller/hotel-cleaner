using NawafizApp.Services.Dtos;
using System.Collections.Generic;

namespace NawafizApp.Services.Interfaces
{
    public interface IFixOrderEqupService
    {
        int add(FixOrderEqupDto fixOrderEqupDto, int orderid);
        List<FixOrderEqupDto> All(int oid);
    }
}