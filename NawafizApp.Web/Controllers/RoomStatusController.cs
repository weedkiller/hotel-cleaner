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
    public class RoomStatusController : BaseAuthorizeController
    {
        IUserService _userService;
        IRoomStatusService _roomStatusService;
        public RoomStatusController(ApplicationUserManager userManager, ApplicationSignInManager aps, IUserService IUS, IRoomStatusService roomStatusService)
         : base(userManager, aps)
        {
            this._userService = IUS;
            this._roomStatusService = roomStatusService;
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]

        public ActionResult AddRoomStatus(int? id,int? statId)
        {
            if (id.HasValue)
            {
                ViewBag.Id = id;
                ViewBag.statId = statId;
                if (statId.HasValue)
                {
                    var dto = _roomStatusService.GetById(statId.Value);
                
                    return View(dto);
                }

                return View();
            }
            else
            {
                return RedirectToAction("AddRoom", "Room");
            }

        }
        [HttpPost]
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoomStatus(RoomStatusDto dto, int? id, int? statId)
        {
            dto.Room_Id = id;
            if (statId.HasValue)
            {
                dto.Id = statId.Value;
                _roomStatusService.Edit(dto);
                return RedirectToAction("AddRoomStatus", new { id = id.Value, statId = statId.Value });

            }
            else
            {
                int i = _roomStatusService.Add(dto);
                return RedirectToAction("AddRoomStatus", new { id = id.Value, statId = i });
                
            }
           
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]

        public ActionResult getAllRoomStatus(int? roomstatNum = -100, string roomstat = "", int? id = -1)
        {
            List<RoomStatusDto> list = new List<RoomStatusDto>();
            if (id != -1)
            {
                list = _roomStatusService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).ToList();
            }

            if (Request.IsAjaxRequest())
            {
                if (roomstatNum == -100 && !string.IsNullOrWhiteSpace(roomstat))
                {

                    return PartialView(_roomStatusService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).Where(x => x.Status.Contains(roomstat)));
                }
                if (roomstatNum != -100 && string.IsNullOrWhiteSpace(roomstat))
                {
                    return PartialView(_roomStatusService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).Where(x => x.RoomNum == roomstatNum.Value.ToString()));
                }
                if (roomstatNum != -100 && !string.IsNullOrWhiteSpace(roomstat))
                {
                    return PartialView(_roomStatusService.All(Convert.ToInt32(id)).OrderByDescending(x => x.Id).Where(x => x.Status.Contains(roomstat) && x.RoomNum == roomstatNum.Value.ToString()));
                }
            }
            return View(list);
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult deleteRoomStatus(int id, int? rId)
        {
            var x = _roomStatusService.RoomStatusRemove(id);
            if (x)
                return RedirectToAction("AddRoomStatus", new { id = rId });
            return RedirectToAction("Error");
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult Edit(int id, int? rid)
        {
            ViewBag.rid = rid.Value;
            var dto = _roomStatusService.GetById(id);
            return View(dto);
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoomStatusDto roomStatusDto, int? rid)
        {

            if (ModelState.IsValid)
            {

                _roomStatusService.Edit(roomStatusDto);
                return RedirectToAction("AddRoomStatus", "RoomStatus", new { id = rid });

            }
            return View(roomStatusDto);
        }
    }
}