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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid Add(UserDto dto)
        {
            var model = Mapper.Map<UserDto, User>(dto);
            _unitOfWork.UserRepository.Add(model);
            _unitOfWork.SaveChanges();
            return model.UserId;
        }
        public void editForAdm(Guid id, string fullname, string userName)
        {
            var user = _unitOfWork.UserRepository.FindById(id);
            user.UserName = userName;
            user.FullName = fullname;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }
        public bool Edit(RegisterUserDto dto,Guid id)
        {
            User user = _unitOfWork.UserRepository.FindById(id);
            if (user == null)
                return false;
            if (!string.IsNullOrWhiteSpace(dto.FullName))
            {
                user.FullName = dto.FullName;
            }
            if (!string.IsNullOrWhiteSpace(dto.UserName))
            { 
                user.UserName = dto.UserName;
            }
         
            if (!string.IsNullOrWhiteSpace(dto.Contract))
            {
                user.Contract = dto.Contract;
            }
            if (!string.IsNullOrWhiteSpace(dto.Image))
            {
                user.Image = dto.Image;
            }
            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                user.PhoneNumber = dto.PhoneNumber;
            }
            if (!string.IsNullOrWhiteSpace(dto.Mobile))
            {
                user.Mobile = dto.Mobile;
            }
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                user.Email = dto.Email;
            }
            user.PassWordExpired = dto.PassWordExpired;
            if (!string.IsNullOrWhiteSpace(dto.NationalNum))
            {
                user.NationalNum = dto.NationalNum;
            }

            user.IsBusy = dto.IsBusy;

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            User user = _unitOfWork.UserRepository.FindById(id);
            if (user == null)
                return false;
            _unitOfWork.UserRepository.Remove(user);
            _unitOfWork.SaveChanges();
            return true;
        }

        public RegisterUserDto GetById(Guid id)
        {
            var dto = _unitOfWork.UserRepository.FindById(id);
            RegisterUserDto user = new RegisterUserDto();
            user.UserId = dto.UserId;
            user.FullName = dto.FullName;
            user.UserName = dto.UserName;
            user.CreationDate = dto.CreationDate;
            user.Contract = dto.Contract;
            user.Image = dto.Image;
            user.PhoneNumber = dto.PhoneNumber;
            user.Mobile = dto.Mobile;
            user.Email = dto.Email;
            user.PassWordExpired = dto.PassWordExpired;
            user.NationalNum = dto.NationalNum;
            user.UserId = dto.UserId;

            if (dto.Roles.Count>0 && dto.Roles.Count==1 )
            {
                user.role = dto.Roles.Single().Name;

            }
            
            return user;
        }
        //[Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]

        public List<UserDto> GetAll()
        {
        var x =  Mapper.Map<List<User>, List<UserDto>>(_unitOfWork.UserRepository.GetAll());
            
            foreach(var i in x)
            {
               if (HasRole(i.UserId, "HouseKeep")) { i.Role = "HouseKeep"; }
                if (HasRole(i.UserId, "Reception"))
                {
                    i.Role = "موظف استقبال"; 
                }
                 if (HasRole(i.UserId, "Hoster"))
                {
                     i.Role = "موظف حجز"; 
                }
                 if (HasRole(i.UserId, "MaintenanceEmp"))
                {
                     i.Role = "موظف صيانة"; 
                }
                 if (HasRole(i.UserId, "Cleaner"))
                {
                     i.Role = "موظف نظافة"; 
                }
                if (HasRole(i.UserId, "BlockSupervisor"))
                {
                    i.Role = "مشرف";
                }
                if (HasRole(i.UserId, "service"))
                {
                    i.Role = "موظف خدمة";
                }   
            }
            return x;
            
        }

        public List<UserDto> usersforblock(int bid)
        {
            var list =_unitOfWork.UserRepository.GetAll().Where(x => x.HotelBlock.Id == bid);
            List<UserDto> userDtol = new List<UserDto>();
            UserDto userDtos = new UserDto();
            foreach (var item in list)
            {
                userDtos.UserId = item.UserId;
                userDtos.UserName = item.UserName;
                userDtos.Role = item.Roles.ToString();
                userDtol.Add(userDtos);
                userDtos = new UserDto();
            }


            return (userDtol);
        }
        public bool HasRole(Guid id,String role)
        {
            User u = _unitOfWork.UserRepository.FindById(id);
            foreach(var i in u.Roles)
            {
                if (i.Name == role) return true;
            }
            return false;
        }
        public bool Exists(Guid id)
        {
            return GetById(id) == null ? false : true;
        }


        public List<UserDto> GetUsersByRole(string role)
        {
            return GetAll().AsEnumerable().Where(u => HasRole(u.UserId, role)).ToList();
        }

        public bool IsEmailUnique(string email)
        {
            return _unitOfWork.UserRepository.FindByEmail(email.ToLower()) == null;
        }

        public bool isUserNameValid(Guid id, string userName)
        {
            var list = _unitOfWork.UserRepository.FindBy(x => x.UserId != id).ToList();
            var semaphore = list.Where(x => x.UserName == userName).SingleOrDefault();
            if (semaphore==null) { return true; }
            return false;
        }
    }
}
