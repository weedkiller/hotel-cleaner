using NawafizApp.Domain;
using NawafizApp.Domain.Entities;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public List<CleanOrderDto> getAllOrderReport(int? id)
        {
            List<CleanOrder> list = _unitOfWork.OrderRepository.GetAll();
            List<CleanOrderDto> dtos = new List<CleanOrderDto>();
            CleanOrderDto dto = new CleanOrderDto();
            foreach (var item in list)
            {
                dto.Id = item.Id;

                dto.Description = item.Description;
                dto.Creation_At = item.Creation_At;
                dto.Creation_Date = item.Creation_Date;
                dto.Creation_Time = item.Creation_Time;
                dto.isFinished = item.isFinished;
                dto.startdate = item.startdate;
                dto.enddate = item.enddate;
              
                if (item.Room != null)
                {

                    dto.Room_ID = item.Room.Id;
                }
                dtos.Add(dto);
                dto = new CleanOrderDto();

            }
            return dtos;
        }

        //public PercentDto getAllRoomsStatusePercentInHotelBlock(int? id)
        //{
        //    PercentDto dt = new PercentDto();
        //    dt.numberofRoom = _unitOfWork.RoomRepository.GetAll().Where(x => x.HotelBlock_ID == id).ToList().Count().ToString();
        //    dt.emptyandcleanofRoom = _unitOfWork.RoomRepository.GetAll().Where(x => x.HotelBlock_ID == id&&x.RoomStatus?.Status == "فارغة و بحاجة تنظيف").ToList().Count().ToString();
        //    dt.readyofRoom = _unitOfWork.RoomRepository.GetAll().Where(x => x.HotelBlock_ID == id&& x.RoomStatus?.Status == "جاهزة ").ToList().Count().ToString();
        //    dt.PuseandcleanofRoom = _unitOfWork.RoomRepository.GetAll().Where(x => x.HotelBlock_ID == id && x.RoomStatus?.Status == "مشغولة و بحاجة تنظيف").ToList().Count().ToString();
        //    dt.PuseofRoom = _unitOfWork.RoomRepository.GetAll().Where(x => x.HotelBlock_ID == id && x.RoomStatus?.Status == "مشغولة").ToList().Count().ToString();
        //    return dt;
        //}

        public List<RoomDto> getAllRoomsWithStatusInHotelBlock(int? id)
        {
            RoomDto dto = new RoomDto();
            List<RoomDto> listdto = new List<RoomDto>();

            var list = _unitOfWork.RoomRepository.GetAll().Where(x=>x.HotelBlock.Id==id).ToList();
            foreach (var item in list)
            {
                dto.Id = item.Id;
                dto.RoomNum = item.RoomNum;
                if (item.HotelBlock != null)
                {
                    dto.RoomDirection = item.RoomDirection;
                    dto.HotelBlock_id = item.HotelBlock.Id;
                }
                //if (item.RoomStatus != null)
                //{
                //    dto.RoomStatus.Id = item.RoomStatus.Id;

                //}
                if (item.HotelBlock != null)
                {
                    dto.HotelBlock_id = item.HotelBlock.Id;
                }

                listdto.Add(dto);
                dto = new RoomDto();
            }
            return listdto;
        }
        public List<RoomDto> getAllRooms()
        {
            RoomDto dto = new RoomDto();
            List<RoomDto> listdto = new List<RoomDto>();

            var list = _unitOfWork.RoomRepository.GetAll().ToList();
            foreach (var item in list)
            {
                dto.Id = item.Id;
                dto.RoomNum = item.RoomNum;
                //if (item.HotelBlock != null)
                //{
                //    dto.RoomDirection = item.RoomDirection;
                //    dto.HotelBlock.Id = item.HotelBlock.Id;
                //}
                ////if (item.RoomStatus != null)
                ////{
                ////    dto.RoomStatus.Id = item.RoomStatus.Id;

                ////}
                //if (item.HotelBlock != null)
                //{
                //    dto.HotelBlock.BlockName = item.HotelBlock.BlockName;
                //}

                listdto.Add(dto);
                dto = new RoomDto();
            }
            return listdto;
        }
    }
    }

