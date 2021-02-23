using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using NawafizApp.Services.Interfaces;
using NawafizApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NawafizApp.Web.Controllers
{
    public class RoomController : BaseAuthorizeController
    {
        IUserService _userService;
        IRoomService _roomService;
        IRoomTypeService _roomTypeService;
        IHotelBlockService _hotelBlockService;
        IRoomRecServices _RoomRecServices;
        public RoomController(IRoomRecServices RoomRecServices,ApplicationUserManager userManager, ApplicationSignInManager aps, IUserService IUS, IRoomService roomService,IRoomTypeService roomTypeService,IHotelBlockService hotelBlockService)
            : base(userManager, aps)
        {
            _RoomRecServices = RoomRecServices;
            this._userService = IUS;
            this._roomService = roomService;
            _roomTypeService = roomTypeService;
            _hotelBlockService = hotelBlockService;
        }
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult AddRoom()
        {

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult AddRoom(RoomDto dto, int hid ,int tid)
        {
            dto.RoomType_id = tid;
            dto.HotelBlock_id = hid;
            int i = _roomService.Add(dto);
            roomrecDto roomrecDto = new roomrecDto();
            roomrecDto.Room_Id = i;
            roomrecDto.Recoed = "تم أضافة الغرفة ";
            _RoomRecServices.Add(roomrecDto);
   
            return RedirectToAction("AddEquipment", "Equipment",new { Rid = i });
        }


        //public ActionResult getAllRooms(int? roomNum = -100, string roomDesc ="",int? id = -1)
        //{
        //    List<RoomDto> list = new List<RoomDto>();
        //    if (id!=-1)
        //    {
        //         list = _roomService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).ToList();
        //    }

        //    if (Request.IsAjaxRequest())
        //    {
        //        if (roomNum == -100 && !string.IsNullOrWhiteSpace (roomDesc))
        //        {

        //            return PartialView(_roomService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).Where(x => x.RoomDirection.Contains(roomDesc) ));
        //        }
        //        if (roomNum != -100 && string.IsNullOrWhiteSpace(roomDesc))
        //        {
        //            return PartialView(_roomService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).Where(x => x.RoomNum==roomNum.Value.ToString() ));
        //        }
        //        if (roomNum != -100 && !string.IsNullOrWhiteSpace(roomDesc))
        //        {
        //            return PartialView(_roomService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).Where(x => x.RoomDirection.Contains(roomDesc) && x.RoomNum == roomNum.Value.ToString() ));
        //        }
        //    }
        //    return View(list);
        //}

        //[Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult RoomView()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Hoster")]

        public ActionResult getAllRoom()
        {
            List<RoomDto> list1 = _roomService.GetAll();
            List<RoomDto> list2 = new List<RoomDto>();
            foreach (var item in list1)
            {

                item.TypeName = _roomTypeService.getbyidGetById(item.RoomType_id).Name.ToString();
                item.blockName = _hotelBlockService.GetById(item.HotelBlock_id).BlockName.ToString();
                list2.Add(item);
            }



            return View(list2);

        }


        //if (Request.IsAjaxRequest())
        //{
        //    if (roomNum == -100 && !string.IsNullOrWhiteSpace(roomDesc))
        //    {

        //        return PartialView(_roomService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).Where(x => x.RoomDirection.Contains(roomDesc)));
        //    }
        //    if (roomNum != -100 && string.IsNullOrWhiteSpace(roomDesc))
        //    {
        //        return PartialView(_roomService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).Where(x => x.RoomNum == roomNum.Value.ToString()));
        //    }
        //    if (roomNum != -100 && !string.IsNullOrWhiteSpace(roomDesc))
        //    {
        //        return PartialView(_roomService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).Where(x => x.RoomDirection.Contains(roomDesc) && x.RoomNum == roomNum.Value.ToString()));
        //    }
        //}
        //return View(list);

        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult Delete(int id)
        {
            var x = _roomService.RoomRemove(id);
            if (x)
                return RedirectToAction("getAllRoom");
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult Edit(int rid)
        {
           
            var dto = _roomService.GetById(rid);
            return View(dto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult Edit(RoomDto roomDto)
        {

            if (ModelState.IsValid)
            {

                _roomService.Edit(roomDto);

                roomrecDto roomrecDto = new roomrecDto();
                roomrecDto.Room_Id =roomDto.Id;
                roomrecDto.Recoed = "تم تعديل  الغرفة ";
                _RoomRecServices.Add(roomrecDto);
                return RedirectToAction("getAllRoom", "Room");

            }
            return View(roomDto);
        }
    }
}