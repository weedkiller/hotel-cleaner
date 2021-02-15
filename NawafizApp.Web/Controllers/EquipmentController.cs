using NawafizApp.Common;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using NawafizApp.Services.Interfaces;
using NawafizApp.Services.Services;
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

        public ActionResult AddEquipment()
        {


            return View();


        }
        [HttpPost]
        [Authorize(Roles = "Admin,Hoster")]

        public ActionResult AddEquipment(EquipmentDto dto, int rid)
        {

            dto.Room_id = rid;


            int i = _equipmentService.Add(dto);


            return View();


        }

        [Authorize(Roles = "Admin,Hoster")]

        public ActionResult getAllEquipments(int rid)
        {


            return View(_equipmentService.All(rid));

        }




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
        [Authorize(Roles = "Admin,Hoster")]


        public ActionResult Check(int id)
        {

            var dto = _equipmentService.GetById(id);
            return View(dto);

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult Check(EquipmentDto eqDto)
        {

            if (ModelState.IsValid)
            {

                _equipmentService.Edit(eqDto);
                if (eqDto.needfix==true)
                {
                  var room=  _roomService.GetById(eqDto.Room_id);
                    room.IsNeedfix = true;
                    _roomService.Edit(room);
                }

              
                return RedirectToAction("getAllEquipments");


            }
            return View(eqDto);

        }
    }
}