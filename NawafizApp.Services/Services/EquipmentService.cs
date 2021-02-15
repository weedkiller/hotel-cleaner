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
using System.Threading.Tasks;

namespace NawafizApp.Services.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EquipmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public int Add(EquipmentDto dto)
        {
            
             var model = Mapper.Map<EquipmentDto, Equipment>(dto);
             model.Room = _unitOfWork.RoomRepository.FindById(dto.Room_id);
            _unitOfWork.EquipmentRepository.Add(model);
            _unitOfWork.SaveChanges();

            return model.Id;
        }

        public List<EquipmentDto> All(int rid)
        {



            var list = _unitOfWork.EquipmentRepository.GetAll().Where(x => x.Room.Id == rid).ToList();


            return Mapper.Map<List<Equipment>, List<EquipmentDto>>(list);
            
        }

        public bool Edit(EquipmentDto dto)
        {
            Equipment n = _unitOfWork.EquipmentRepository.FindById(dto.Id);
            n.Id = dto.Id;

            n.Name = dto.Name;
            n.Description = dto.Description;
            n.Stauts = dto.Stauts;
            n.needfix = dto.needfix;
            n.ishere = dto.ishere;
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool EquipmentRemove(int Id)
        {
            var n = _unitOfWork.EquipmentRepository.FindById(Id);
            if (n == null)
            {
                return false;
            }

            _unitOfWork.EquipmentRepository.Remove(n);
            _unitOfWork.SaveChanges();
            return true;
        }

        public EquipmentDto GetById(int id)
        {
            var list = Mapper.Map<Equipment, EquipmentDto>(_unitOfWork.EquipmentRepository.FindById(id));
            //if (_unitOfWork.EquipmentRepository.FindById(id).Rooms.Count>0)
            //{
            //    list.Room_Id = _unitOfWork.EquipmentRepository.FindById(id).Rooms.Where(x => x.Id == rid.Value).SingleOrDefault().Id;
            //}
            return list;
        }
        public void checkedToggle(int id)
        {
            var model = _unitOfWork.EquipmentRepository.FindById(id);
            model.ishere = !model.ishere;
            _unitOfWork.EquipmentRepository.Update(model);
            _unitOfWork.SaveChanges();
        }
    }
}
