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
    public class CleanOrderService : ICleanOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CleanOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public int addOrder(CleanOrderDto dto)
        {
            CleanOrder Order = new CleanOrder();
            Order.Hoster = dto.Hoster;
            Order.moshId = dto.moshId;
            Order.cleaningEmp = dto.cleaningEmp;
            
            Order.Description = dto.Description;
            Order.Creation_Date = dto.Creation_Date ;
            Order.Creation_Time = dto.Creation_Time;
            Order.Creation_At = dto.Creation_At;

         
            

            // Order.moshId = _unitOfWork.RoleRepository.FindByName("mosh").Users.Where(x => x.HotelBlock.Rooms == _unitOfWork.RoomRepository.FindById(dto.Room_ID).Id); ;

              //  Order.User = _unitOfWork.UserRepository.FindById(dto.User_ID);
         
            if (dto.Room_ID.HasValue)
            {
                Order.Room = _unitOfWork.RoomRepository.FindById(dto.Room_ID);
            }
            _unitOfWork.OrderRepository.Add(Order);
            _unitOfWork.SaveChanges();
            return Order.Id;
        }

        public bool delete(int id)
        {
            var i = _unitOfWork.OrderRepository.FindById(id);
            if (i == null) return false;
    

            _unitOfWork.OrderRepository.Remove(i);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool edit(CleanOrderDto dto)
        {
            CleanOrder Order = _unitOfWork.OrderRepository.FindById(dto.Id);

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                Order.Description = dto.Description;
            }

            if (!string.IsNullOrWhiteSpace(dto.startdate))
            {
                Order.startdate = dto.startdate;
            }
            if (!string.IsNullOrWhiteSpace(dto.enddate))
            {
                Order.enddate = dto.enddate;
            }
            if (!string.IsNullOrWhiteSpace(dto.Creation_At))
            {
                Order.Creation_At = dto.Creation_At;
            }
            if (!string.IsNullOrWhiteSpace(dto.Creation_Date))
            {
                Order.Creation_Date = dto.Creation_Date;
            }
            if (!string.IsNullOrWhiteSpace(dto.Creation_Time))
            {
                Order.Creation_Time = dto.Creation_Time;
            }
            


               
            Order.isFinished = dto.isFinished;
            if (dto.cleaningEmp.HasValue)
            {
                Order.cleaningEmp = dto.cleaningEmp;
            }
            if (dto.moshId.HasValue)
            {
                Order.moshId = dto.moshId;
            }
            if (dto.Hoster.HasValue)
            {
                Order.Hoster = dto.Hoster;
            }
            
            Order.isTaked = dto.Istaked;
            

            
       
       
               // Order.User = _unitOfWork.UserRepository.FindById(dto.User_ID);
         
            if (dto.Room_ID.HasValue)
            {
                Order.Room = _unitOfWork.RoomRepository.FindById(dto.Room_ID);
            }
            _unitOfWork.OrderRepository.Update(Order);
            
            _unitOfWork.SaveChanges();
            return true;
        }

        public List<CleanOrderDto> GetAll()
        {
            List<CleanOrder> list = _unitOfWork.OrderRepository.GetAll();
            List<CleanOrderDto> dtos = new List<CleanOrderDto>();
            CleanOrderDto dto = new CleanOrderDto();
            foreach (var item in list)
            {
                dto.Id = item.Id;
                dto.Istaked = item.isTaked;


                dto.moshId = item.moshId;
                dto.Hoster = item.Hoster;
                dto.startdate = item.startdate;
                dto.enddate = item.enddate;
                dto.cleaningEmp = item.cleaningEmp;

                dto.isFinished = item.isFinished;
                dto.Description = item.Description;
                dto.Creation_At = item.Creation_At;
                dto.Creation_Date = item.Creation_Date;
                dto.Creation_Time = item.Creation_Time;
                
               
                if (item.Room!=null)
                {

                    dto.Room_ID = item.Room.Id;
                }
                dtos.Add(dto);
                dto = new CleanOrderDto();

            }
            return dtos;
        }

        public CleanOrderDto GetById(int id)
        {
                var item = _unitOfWork.OrderRepository.FindById(id);
                CleanOrderDto dto = new CleanOrderDto();
                dto.Id = item.Id;
                dto.Description = item.Description;
                dto.Creation_At = item.Creation_At;
                dto.Creation_Date = item.Creation_Date;
                dto.Creation_Time = item.Creation_Time;
                dto.isFinished = item.isFinished;
                dto.moshId = item.moshId;
                dto.cleaningEmp = item.cleaningEmp;
                dto.Hoster = item.Hoster;
            dto.isFinished = item.isFinished;
            dto.Istaked = item.isTaked;
            dto.startdate = item.startdate;
            dto.enddate = item.enddate;

                


             
                dto.Room_ID = item.Room.Id;
            return dto;
        }


        public Guid getmoshbyroomId(int rid)
        {
            var ie = _unitOfWork.OrderRepository.getManIdforRoom(rid);

            return ie;

                
        }

        public bool setIsSeenTrue()
        {

            List<CleanOrder> list = _unitOfWork.OrderRepository.GetAll();
            foreach (var item in list)
            {
                item.IsSeenFromCleaner = true;

                _unitOfWork.OrderRepository.Update(item);
            }

            _unitOfWork.SaveChanges();
            return true;
        }
        public bool setIsSeenTrueForMosherf()
        {

            List<CleanOrder> list = _unitOfWork.OrderRepository.GetAll();
            foreach (var item in list)
            {
                item.IsSeenFromManager = true;

                _unitOfWork.OrderRepository.Update(item);
            }

            _unitOfWork.SaveChanges();
            return true;
        }
        
    }
}
