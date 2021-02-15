using AutoMapper;
using NawafizApp.Domain;
using NawafizApp.Domain.Entities;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Services
{
    public class RoomStatusService : IRoomStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public int Add(RoomStatusDto dto)
        {
            RoomStatus n = new RoomStatus();

            n.Id = dto.Id;
            n.StatusNum = dto.StatusNum;

            n.Rooms = new List<Room>();
            n.Rooms.Add(_unitOfWork.RoomRepository.FindById(dto.Room_Id));
            _unitOfWork.RoomStatusRepository.Add(n);
            _unitOfWork.SaveChanges();
            return n.Id;
        }

        public List<RoomStatusDto> All(int? rid)
        {
            RoomStatusDto dto = new RoomStatusDto();
            List<RoomStatusDto> listdto = new List<RoomStatusDto>();
            var list = _unitOfWork.RoomStatusRepository.GetAll().Where(x => x.Rooms.Contains(_unitOfWork.RoomRepository.FindById(rid))).ToList();
            foreach (var item in list)
            {
                dto.Id = item.Id;
                dto.StatusNum = item.StatusNum;

                if (item.Rooms != null)
                {
                    foreach (var item1 in item.Rooms)
                    {
                        dto.RoomNum = item1.RoomNum;
                        dto.Room_Id = item1.Id;
                    }
                }
                listdto.Add(dto);
                dto = new RoomStatusDto();
            }
            return listdto;
        }

        public List<RoomStatusDto> getall()
        {
            RoomStatusDto dto = new RoomStatusDto();
            List<RoomStatusDto> listdto = new List<RoomStatusDto>();
            var list = _unitOfWork.RoomStatusRepository.GetAll().ToList();
            foreach (var item in list)
            {
                dto.Id = item.Id;
                dto.StatusNum = item.StatusNum;

                if (item.Rooms != null)
                {
                    foreach (var item1 in item.Rooms)
                    {
                        dto.RoomNum = item1.RoomNum;
                        dto.Room_Id = item1.Id;
                    }
                }
                listdto.Add(dto);
                dto = new RoomStatusDto();
            }
            return listdto;
        }

        public bool Edit(RoomStatusDto dto)
        {
            RoomStatus n = _unitOfWork.RoomStatusRepository.FindById(dto.Id);
   
                n.Id = dto.Id;
      
           
            n.StatusNum = dto.StatusNum;

            _unitOfWork.RoomStatusRepository.Update(n);
            _unitOfWork.SaveChanges();
            return true;
        }

        public RoomStatusDto GetById(int id)
        {
            var list = _unitOfWork.RoomStatusRepository.FindById(id);
            return Mapper.Map<RoomStatus, RoomStatusDto>(list);
        }

        public bool RoomStatusRemove(int Id)
        {
            var n = _unitOfWork.RoomStatusRepository.FindById(Id);
            if (n == null)
            {
                return false;
            }
            n.Rooms.Clear();
            _unitOfWork.RoomStatusRepository.Remove(n);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
