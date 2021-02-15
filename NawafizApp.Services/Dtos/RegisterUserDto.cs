using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos
{
    public class RegisterUserDto
    {
   
        public string UserName { get; set; }
        public bool IsBusy { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string NationalNum { set; get; }
        public string Image { set; get; }
        public string Contract { set; get; }
        public string Mobile { set; get; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool PassWordExpired { set; get; }
        public DateTime CreationDate { get; set; }
        public bool wantEditPassword { set; get; }
        public string ConfirmPassword { get; set; }
        public Guid UserId { set; get; }
        public string role { set; get; }
    }
}
