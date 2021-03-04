using NawafizApp.Common;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using NawafizApp.Services.Interfaces;
using NawafizApp.Services.Services;
using NawafizApp.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawafizApp.Web.Controllers
{
    public class EquipmentController : BaseAuthorizeController
    {
        IUserService _userService;
        IEquipmentService _equipmentService;
        IFixOrderServices _fixOrderService;
        IRoomService _roomService;

        public EquipmentController(ApplicationUserManager userManager, ApplicationSignInManager aps, IUserService IUS, IEquipmentService equipmentService, IRoomService roomService)
         : base(userManager, aps)
        {
            _roomService = roomService;
            this._userService = IUS;
            this._equipmentService = equipmentService;
        }
        [Authorize(Roles = "Admin,Hoster")]

        public ActionResult AddEquipment(int rid)
        {


            return View();


        }
        [HttpPost]
        [Authorize(Roles = "Admin,Hoster")]

        public ActionResult AddEquipment(EquipmentDto dto, int rid)
        {

            dto.Room_id = rid;


            int i = _equipmentService.Add(dto);

            ViewBag.Rid = rid;
            return RedirectToAction("AddEquipment",new {rid=rid });


        }

        [Authorize(Roles = "Admin,Hoster,Cleaner")]

        public ActionResult getAllEquipments(int Rid)
        {

            ViewBag.Rid = Rid;
            return View(_equipmentService.All(Rid));

        }



        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult Delete(int id)
        {
            var x = _equipmentService.EquipmentRemove(id);

            return RedirectToAction("getAllRoom", "Room");

        }

        [Authorize(Roles = "Admin,Hoster")]

        public ActionResult Edit(int id)
        {

            var dto = _equipmentService.GetById(id);
            return View(dto);

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult Edit(EquipmentDto eqDto)
        {

            if (ModelState.IsValid)
            {

                _equipmentService.Edit(eqDto);
                return RedirectToAction("getAllRoom", "Room");

            }
            return View(eqDto);
        }
        [Authorize(Roles = "Admin,Hoster,Cleaner")]


        public ActionResult getAllEquipmentsForcleaningEmp(int Rid)
        {

            var dto = _equipmentService.All(Rid);
            return View(dto);

        }


        public ActionResult CheckedToggle(int id,int Rid)
        {
            var roomsStatusChangedCount = (int)int.Parse(System.Web.HttpContext.Current.Session["roomsStatusChangedCount"].ToString());
            _equipmentService.checkedToggle(id);
            var room = _roomService.GetById(Rid);
            room.IsNeedfix = !room.IsNeedfix;
            if (room.IsNeedfix && !room.Isrequistedfix)
                roomsStatusChangedCount++;
            _roomService.Edit(room);
            System.Web.HttpContext.Current.Session["roomsStatusChangedCount"] = roomsStatusChangedCount;
            SignalHelper.SendRoomsStatusChangedCount(roomsStatusChangedCount);

            MysqlFetchingRoomData.SetFixStatus(room.RoomNum, room.IsNeedfix);
            return RedirectToAction("getAllEquipmentsForcleaningEmp", new { Rid = Rid });



        }

        public ActionResult CheckedToggleFix(int id, int Rid)
        {
            var roomsStatusChangedCount = (int)int.Parse(System.Web.HttpContext.Current.Session["roomsStatusChangedCount"].ToString());
            _equipmentService.checkedToggleFix(id);
            var room = _roomService.GetById(Rid);
            room.IsNeedfix = !room.IsNeedfix;
            if (room.IsNeedfix && !room.Isrequistedfix)
                roomsStatusChangedCount++;
            _roomService.Edit(room);
            System.Web.HttpContext.Current.Session["roomsStatusChangedCount"] = roomsStatusChangedCount;
            SignalHelper.SendRoomsStatusChangedCount(roomsStatusChangedCount);

            MysqlFetchingRoomData.SetFixStatus(room.RoomNum, room.IsNeedfix);
            return RedirectToAction("getAllEquipmentsForcleaningEmp", new { Rid = Rid }); 



        }

    }
}