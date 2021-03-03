using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Dtos
{
    public class HotelBlockDto
    {
        public HotelBlockDto()
        {
            this.Supervisors = new List<SupervisorDto>();
        }
        public int Id { set; get; }
        public string BlockNum { set; get; }
        public string BlockName { set; get; }
        public string BlockDescription { set; get; }
        public string NmberOfTheFloorsIntheBlock { set; get; }
        public string NmberOfTheRoomsIntheBlock { set; get; }
        public List<string> Ids { set; get; }
        public List<SupervisorDto> Supervisors { set; get; }
        public List<ShowUsersOfHotelBlockByRoleDto> userInRoleLists { set; get; }


    }
    public class SupervisorDto{

        public string Id { set; get; }
        public DateTime? FromTime { set; get; }
        public DateTime? ToTime { set; get; }
    }
}
