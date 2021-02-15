using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsBusy { get; set; }
        public string NationalNum { set; get; }
        public string Image { set; get; }
        public string Contract { set; get; }
        public string Mobile { set; get; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Role { get; set; }
        public bool PassWordExpired { set; get; }
        public int Hotelblock_id { get; set; }
    }
}
