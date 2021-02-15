using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> usersforblock(int bid);
        Guid Add(UserDto dto);
        bool Edit(RegisterUserDto dto,Guid id);
        bool Delete(Guid id);
        RegisterUserDto GetById(Guid id);
        List<UserDto> GetAll();
        bool isUserNameValid(Guid id, string userName);
        bool HasRole(Guid id, String role);
        List<UserDto> GetUsersByRole(string role);
        bool Exists(Guid id);
        bool IsEmailUnique(string email);
        void editForAdm(Guid id, string fullname, string userName);
    }
}
