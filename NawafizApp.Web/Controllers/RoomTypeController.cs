using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawafizApp.Web.Controllers
{
    public class RoomTypeController : BaseAuthorizeController
    {
        IUserService _userService;
        IRoomTypeService _roomTypeService;


        public RoomTypeController(ApplicationUserManager userManager, ApplicationSignInManager aps, IUserService IUS, IRoomTypeService roomTypeService)
               : base(userManager, aps)
        {
            this._userService = IUS;
            this._roomTypeService = roomTypeService;
        }


        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Add(RoomTypeDto roomTypeDto)
        {
            _roomTypeService.Add(roomTypeDto);
            
            return RedirectToAction("getAll", "RoomType");
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult Edit(int id)
        {

            var dto = _roomTypeService.getbyidGetById(id);
            return View(dto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit(RoomTypeDto roomTypeDto)
        {

            if (ModelState.IsValid)
            {

                _roomTypeService.Edit(roomTypeDto);
                return RedirectToAction("getall", "RoomType");

            }
            return View(roomTypeDto);
        }

        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]

        public ActionResult getall()
        {
            return View(_roomTypeService.GetRoomTypes());



        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]

       
        public ActionResult Delete(int?id)
        {
            _roomTypeService.delete(id.Value);
            return RedirectToAction("getall");
        }


    }
}