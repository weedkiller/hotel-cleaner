using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;

namespace NawafizApp.Services.Interfaces
{
    public interface IFixOrderServices
    {
        int addFixOrder(FixOrderDto dto);
        Guid getmoshbyroomId(int rid);
        List<FixOrderDto> GetAll();
        bool edit(FixOrderDto dto);
        FixOrderDto GetById(int id);


    }
}