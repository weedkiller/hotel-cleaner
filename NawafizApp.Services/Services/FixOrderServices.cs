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
    public class FixOrderServices : IFixOrderServices
    {
        IUnitOfWork _unitOfWork;
        public FixOrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public int addFixOrder(FixOrderDto dto)
        {
            FixOrder Order = new FixOrder();
            Order.Hoster = dto.Hoster;
            Order.moshId = dto.moshId;
            Order.maitremp = dto.maitremp;

            Order.Description = dto.Description;
            Order.Creation_Date = dto.Creation_Date;
            Order.Creation_Time = dto.Creation_Time;
            Order.Creation_At = dto.Creation_At;

            if (dto.Room_ID.HasValue)
            {
                Order.Room = _unitOfWork.RoomRepository.FindById(dto.Room_ID);
            }
            _unitOfWork.FixOrderRepository.Add(Order);
            _unitOfWork.SaveChanges();
            return Order.Id;








        }

        public Guid getmoshbyroomId(int rid)
        {
            var ie = _unitOfWork.OrderRepository.getManIdforRoom(rid);

            return ie;


        }


        public List<FixOrderDto> GetAll()
        {
            List<FixOrder> list = _unitOfWork.FixOrderRepository.GetAll();
            List<FixOrderDto> dtos = new List<FixOrderDto>();
            FixOrderDto dto = new FixOrderDto();
            foreach (var item in list)
            {
                dto.Id = item.Id;
                dto.moshId = item.moshId;
                dto.Hoster = item.Hoster;
                dto.startdate = item.startdate;
                dto.enddate = item.enddate;
                dto.maitremp = item.maitremp;

                dto.isFinished = item.isFinished;
                dto.Description = item.Description;
                dto.Creation_At = item.Creation_At;
                dto.Creation_Date = item.Creation_Date;
                dto.Creation_Time = item.Creation_Time;


                if (item.Room != null)
                {

                    dto.Room_ID = item.Room.Id;
                }
                dtos.Add(dto);
                dto = new FixOrderDto();

            }
            return dtos;
        }
        public FixOrderDto GetById(int id)
        {
            var item = _unitOfWork.FixOrderRepository.FindById(id);
            FixOrderDto dto = new FixOrderDto();
            dto.Id = item.Id;
            dto.Description = item.Description;
            dto.Creation_At = item.Creation_At;
            dto.Creation_Date = item.Creation_Date;
            dto.Creation_Time = item.Creation_Time;
            dto.isFinished = item.isFinished;
            dto.moshId = item.moshId;
            dto.maitremp = item.maitremp;
            dto.Hoster = item.Hoster;
            dto.Room_ID = item.Room.Id;
            return dto;
        }


        public bool edit(FixOrderDto dto)
        {
            FixOrder Order = _unitOfWork.FixOrderRepository.FindById(dto.Id);

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
            Order.Creation_At = dto.Creation_At;
            Order.Creation_Date = dto.Creation_Date;
            Order.Creation_Time = dto.Creation_Time;



            Order.isFinished = dto.isFinished;
            Order.maitremp = dto.maitremp;
            Order.moshId = dto.moshId;
            Order.Hoster = dto.Hoster;
            Order.isTaked = dto.Istaked;





            // Order.User = _unitOfWork.UserRepository.FindById(dto.User_ID);

            if (dto.Room_ID.HasValue)
            {
                Order.Room = _unitOfWork.RoomRepository.FindById(dto.Room_ID);
            }
            _unitOfWork.FixOrderRepository.Update(Order);

            _unitOfWork.SaveChanges();
            return true;
        }
        public bool setIsSeenTrue()
        {

            List<FixOrder> list = _unitOfWork.FixOrderRepository.GetAll();
            foreach (var item in list)
            {
                item.IsSeenFromFixer = true;

                _unitOfWork.FixOrderRepository.Update(item);
            }

            _unitOfWork.SaveChanges();
            return true;
        }
        public bool setIsSeenTrueForMosherf()
        {

            List<FixOrder> list = _unitOfWork.FixOrderRepository.GetAll();
            foreach (var item in list)
            {
                item.IsSeenFromManager = true;

                _unitOfWork.FixOrderRepository.Update(item);
            }

            _unitOfWork.SaveChanges();
            return true;
        }
    }

}
