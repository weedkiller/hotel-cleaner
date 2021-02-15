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
    class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public RoomTypeService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }


        public int Add(RoomTypeDto roomTypeDto)
        {

            RoomType roomType = new RoomType();

            roomType.Name = roomTypeDto.Name;
            roomType.Type = roomTypeDto.Type;

            _unitOfWork.RoomTypeRepository.Add(roomType);
            _unitOfWork.SaveChanges();
            return roomType.Id;



        }


        public bool Edit(RoomTypeDto roomTypeDto)
        {
            RoomType c = _unitOfWork.RoomTypeRepository.FindById(roomTypeDto.Id);
            c.Id = roomTypeDto.Id;
            c.Name = roomTypeDto.Name;
            c.Type = roomTypeDto.Type;
            if (c != null)
            {
                _unitOfWork.RoomTypeRepository.Update(c);
                _unitOfWork.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }

        public List<RoomTypeDto> GetRoomTypes()
        {
            List<RoomTypeDto> list = new List<RoomTypeDto>();
            var dto = _unitOfWork.RoomTypeRepository.GetAll();

            return Mapper.Map<List<RoomType>, List<RoomTypeDto>>(dto);


        }

        public RoomTypeDto getbyidGetById(int id)
        {
            RoomTypeDto roomTypeDto = new RoomTypeDto();
            var model=  _unitOfWork.RoomTypeRepository.FindById(id);
            roomTypeDto.Id = model.Id;
            roomTypeDto.Name = model.Name;
            roomTypeDto.Type = model.Type;


            return roomTypeDto;
        }


        public bool  delete(int id)
        {

            var model = _unitOfWork.RoomTypeRepository.FindById(id);
            if (model != null)
            {
                _unitOfWork.RoomTypeRepository.Remove(model);
                _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
   


        }















    }
}
