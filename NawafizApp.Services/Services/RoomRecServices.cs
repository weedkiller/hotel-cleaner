using AutoMapper;
using NawafizApp.Domain;
using NawafizApp.Domain.Entities;
using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Services
{
    public class RoomRecServices : IRoomRecServices
    {
        IUnitOfWork _unitOfWork;
        public RoomRecServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Add(roomrecDto roomrecDto)
        {
            RoomRec roomRec = new RoomRec();
            roomRec.Room = _unitOfWork.RoomRepository.FindById(roomrecDto.Room_Id);
            roomRec.Recoed = roomrecDto.Recoed;
            roomRec.Datetime = DateTime.Now.TimeOfDay.ToString();
            _unitOfWork.roomrecRepository.Add(roomRec);
            _unitOfWork.SaveChanges();
            return roomRec.Id;

        }

        public List<roomrecDto> getall()
        {
            var dto = _unitOfWork.roomrecRepository.GetAll();


            return Mapper.Map<List<RoomRec>, List<roomrecDto>>(dto);

        }



    }
}
