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
   public class HotelBlockService : IHotelBlockService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HotelBlockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public int addHotelBlock(HotelBlockDto dto)
        {
            HotelBlock HotelBlock = new HotelBlock();
            HotelBlock.BlockNum = dto.BlockNum;
            HotelBlock.BlockName = dto.BlockName;
            HotelBlock.BlockDescription = dto.BlockDescription;
            HotelBlock.NmberOfTheFloorsIntheBlock = dto.NmberOfTheFloorsIntheBlock;
            HotelBlock.NmberOfTheRoomsIntheBlock = dto.NmberOfTheRoomsIntheBlock;
            HotelBlock.Users = new List<User>();
            if (dto.Ids.Count>0)
            {
                foreach (var item in dto.Ids)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        var _user = _unitOfWork.UserRepository.FindById(Guid.Parse(item));
                        if(dto.Supervisors.Any(x => x.Id == item))
                        {
                            _user.FromTime = new DateTime(2000,01,01, dto.Supervisors.FirstOrDefault(x => x.Id == item).FromTime.Value.Hours, dto.Supervisors.FirstOrDefault(x => x.Id == item).FromTime.Value.Minutes,0);
                            _user.ToTime = new DateTime(2000, 01, 01, dto.Supervisors.FirstOrDefault(x => x.Id == item).ToTime.Value.Hours, dto.Supervisors.FirstOrDefault(x => x.Id == item).ToTime.Value.Minutes,0);
                            _unitOfWork.UserRepository.Update(_user);
                        }
                        HotelBlock.Users.Add(_unitOfWork.UserRepository.FindById(Guid.Parse(item)));
                    }
                }
               
            }
            _unitOfWork.HotelBlockRepository.Add(HotelBlock);
            _unitOfWork.SaveChanges();
            return dto.Id;
        }

        public bool delete(int id)
        {
            try
            {
                var i = _unitOfWork.HotelBlockRepository.FindById(id);
                if (i == null) return false;
                i.Users.Clear();
                i.Rooms.Clear();
                var rooms = _unitOfWork.RoomRepository.GetAll().Where(x=> x.HotelBlock.Id == i.Id);
                foreach (var item in rooms)
                {
                    item.HotelBlock = null;
                    _unitOfWork.RoomRepository.Update(item);
                    _unitOfWork.SaveChanges();
                }
                var users = _unitOfWork.UserRepository.GetAll().Where(x => x.HotelBlock.Id == i.Id);
                foreach (var item in users)
                {
                    item.HotelBlock = null;
                    _unitOfWork.UserRepository.Update(item);
                    _unitOfWork.SaveChanges();
                }
                _unitOfWork.HotelBlockRepository.Remove(i);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool edit(HotelBlockDto dto)
        {
            HotelBlock HotelBlock = _unitOfWork.HotelBlockRepository.FindById(dto.Id);
            HotelBlock.BlockNum = dto.BlockNum;
            HotelBlock.BlockName = dto.BlockName;
            HotelBlock.BlockDescription = dto.BlockDescription;
            HotelBlock.NmberOfTheFloorsIntheBlock = dto.NmberOfTheFloorsIntheBlock;
            HotelBlock.NmberOfTheRoomsIntheBlock = dto.NmberOfTheRoomsIntheBlock;
            HotelBlock.Users = new List<User>();
            if (dto.Ids.Count > 0)
            {
                foreach (var userId in dto.Ids)
                {
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        var _user = _unitOfWork.UserRepository.FindById(Guid.Parse(userId));
                        if (dto.Supervisors.Any(x => x.Id == userId))
                        {
                            _user.FromTime = new DateTime(2000, 01, 01, dto.Supervisors.FirstOrDefault(x => x.Id == userId).FromTime.Value.Hours, dto.Supervisors.FirstOrDefault(x => x.Id == userId).FromTime.Value.Minutes, 0);
                            _user.ToTime = new DateTime(2000, 01, 01, dto.Supervisors.FirstOrDefault(x => x.Id == userId).ToTime.Value.Hours, dto.Supervisors.FirstOrDefault(x => x.Id == userId).ToTime.Value.Minutes, 0);
                            _unitOfWork.UserRepository.Update(_user);
                        }
                        if (!HotelBlock.Users.Contains(_unitOfWork.UserRepository.FindById(Guid.Parse(userId))))
                        {
                            HotelBlock.Users.Add(_unitOfWork.UserRepository.FindById(Guid.Parse(userId)));
                        }
                    }
                }

            }
            _unitOfWork.HotelBlockRepository.Update(HotelBlock);
            _unitOfWork.SaveChanges();
            return true;
        }
        public bool HasRole(Guid id, String role)
        {
            User u = _unitOfWork.UserRepository.FindById(id);
            foreach (var i in u.Roles)
            {
                if (i.Name == role) return true;
            }
            return false;
        }
        public List<HotelBlockDto> GetAll()
        {
            List<HotelBlock> list = _unitOfWork.HotelBlockRepository.GetAll();
            List<HotelBlockDto> dtos = new List<HotelBlockDto>();
            HotelBlockDto dto = new HotelBlockDto();
            ShowUsersOfHotelBlockByRoleDto userInRole = new ShowUsersOfHotelBlockByRoleDto();
            List<ShowUsersOfHotelBlockByRoleDto> userInRoleList = new List<ShowUsersOfHotelBlockByRoleDto>();
            foreach (var item in list)
            {
                dto.Id = item.Id;
                dto.BlockNum = item.BlockNum;
                dto.BlockName = item.BlockName;
                dto.BlockDescription = item.BlockDescription;
                dto.NmberOfTheFloorsIntheBlock = item.NmberOfTheFloorsIntheBlock;
                dto.NmberOfTheRoomsIntheBlock = item.NmberOfTheRoomsIntheBlock;
                dtos.Add(dto);
                dto = new HotelBlockDto();
            }
            return dtos;

        }

        public HotelBlockDto GetById(int id)
        {
            HotelBlockDto dto = new HotelBlockDto();
            
            HotelBlock item = _unitOfWork.HotelBlockRepository.FindById(id);
            List<string> userides = new List<string>();
                dto.Id = item.Id;
                dto.BlockNum = item.BlockNum;
                dto.BlockName = item.BlockName;
                dto.BlockDescription = item.BlockDescription;
                dto.NmberOfTheFloorsIntheBlock = item.NmberOfTheFloorsIntheBlock;
                dto.NmberOfTheRoomsIntheBlock = item.NmberOfTheRoomsIntheBlock;
                if (item.Users.Count > 0)
                {
                    foreach (var user in item.Users)
                    {
                    userides.Add(user.UserId.ToString());
                    }
                if (userides!=null)
                {
                    dto.Ids = userides;
                }
                }           
            return dto;
        }
    }
}
