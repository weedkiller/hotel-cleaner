using AutoMapper;
using NawafizApp.Domain;
using NawafizApp.Domain.Entities;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public RoomService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        public int Add(RoomDto dto)
        {

            var model = Mapper.Map<RoomDto, Room>(dto);
            model.HotelBlock = _unitOfWork.HotelBlockRepository.FindById(dto.HotelBlock_id);
            model.RoomType = _unitOfWork.RoomTypeRepository.FindById(dto.RoomType_id);
            model.Isrequisted = false;
            //Room n = new Room();

            //n.Id = dto.Id;
            //n.RoomNum = dto.RoomNum;
            //n.HotelBlock =;

            //n.RoomDirection = dto.RoomDirection;
            //n.RoomType.Id = dto.RoomType;
            //n.Isbusy = dto.Isbusy;
            //n.IsInService = dto.IsInService;
            //n.isneedclean = dto.isneedclean;
            //n.IsNeedfix = dto.IsNeedfix;
         

          
            _unitOfWork.RoomRepository.Add(model);
            _unitOfWork.SaveChanges();
            return model.Id;
        }
        public List<RoomDto> GetAll()
        {
            
            var dto = _unitOfWork.RoomRepository.GetAll();
            
            
            return Mapper.Map<List<Room>, List<RoomDto>>(dto);



            //RoomDto dto = new RoomDto();
            //var lists = _unitOfWork.RoomRepository.GetAll();
            //foreach (var item in lists)
            //{
            //    dto.Id = item.Id;
            //    dto.RoomNum = item.RoomNum;
            //    dto.RoomDirection = item.RoomDirection;
            //    dto.HotelBlock_id = item.HotelBlock.Id;
            //    dto.Isbusy = item.Isbusy;
            //    dto.IsInService = item.IsInService;
            //    dto.isneedclean = item.isneedclean;
            //    dto.IsNeedfix = item.IsNeedfix;
            //    dto.RoomType_id = item.RoomType.Id;



            //    list.Add(dto);
            //    dto = new RoomDto();

            //}
            //return list;
        }
        //public List<RoomDto> All(int?hid)
        //{
        //    //RoomDto dto = new RoomDto();
        //    //List<RoomDto> listdto = new List<RoomDto>();
        //    //var list = _unitOfWork.RoomRepository.GetAll().Where(x=>x.HotelBlock.Id==hid).ToList();
        //    //foreach (var item in list)
        //    //{
        //    //    dto.Id = item.Id;
        //    //    dto.RoomNum = item.RoomNum;
        //    //    if (item.HotelBlock!=null)
        //    //    {
        //    //    dto.RoomDirection = item.RoomDirection;
        //    //    dto.HotelBlock.Id = item.HotelBlock.Id;
        //    //    }
        //    //    if (item.RoomStatus!=null)
        //    //    {
        //    //        dto.RoomStatus.Id = item.RoomStatus.Id;
        //    //    }
        //    //    listdto.Add(dto);
        //    //    dto = new RoomDto();
        //    //}
        //    //return listdto;
        //}

        public bool Edit(RoomDto dto)
        {
            Room n = _unitOfWork.RoomRepository.FindById(dto.Id);
            n.Id = dto.Id;
            n.RoomNum = dto.RoomNum;
            n.RoomDirection = dto.RoomDirection;
            n.RoomType = _unitOfWork.RoomTypeRepository.FindById(dto.RoomType_id);
            n.Isbusy = dto.Isbusy;
            n.IsInService = dto.IsInService;
            n.isneedclean = dto.isneedclean;
            n.IsNeedfix = dto.IsNeedfix;
            n.Isrequisted = dto.Isrequisted;
            n.Isrequistedfix = dto.Isrequistedfix;
            _unitOfWork.RoomRepository.Update(n);

            _unitOfWork.SaveChanges();
            return true;
        }
        //public bool EditRoomStat(int? id, int? stId)
        //{
        //    Room n = _unitOfWork.RoomRepository.FindById(id);
        //    _unitOfWork.RoomRepository.Update(n);

        //    _unitOfWork.SaveChanges();
        //    return true;
        //}
        public RoomDto GetById(int id)
        {
            var list = _unitOfWork.RoomRepository.FindById(id);
            return Mapper.Map<Room, RoomDto>(list);
        }

        public bool RoomRemove(int roomId)
        {
            var n = _unitOfWork.RoomRepository.FindById(roomId);
            if (n == null)
            {
                return false;
            }

            _unitOfWork.RoomRepository.Remove(n);
            _unitOfWork.SaveChanges();
            return true;
        }

        public List<Guid> getReservationEmpIdForRoom(int? id)
        {
            Room n = _unitOfWork.RoomRepository.FindById(id);
            List<Guid> list = new List<Guid>();
            var hid = n.HotelBlock.Id;
            var user = n.HotelBlock.Users;
            foreach (var item in user)
            {
                if (item.Roles.Contains(_unitOfWork.RoleRepository.FindByName("ReservationEmp")))
                {
                    list.Add(item.UserId);
                }
            }
            return list;
        }
        public Guid getMangerIdForRoom(int? id)
        {
            Room n = _unitOfWork.RoomRepository.FindById(id);
           Guid list = new Guid();
            var hid = n.HotelBlock.Id;
            var user = n.HotelBlock.Users;
            foreach (var item in user)
            {
                if (item.Roles.Contains(_unitOfWork.RoleRepository.FindByName("manager")))
                {
                    list = item.UserId;
                }
            }
            return list;
        }

        


    }
}
