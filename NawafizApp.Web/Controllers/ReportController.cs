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
    public class ReportController : BaseAuthorizeController
    {
        IReportService _reportService;
        IHotelBlockService _hotelBlockService;
        public ReportController(ApplicationUserManager userManager, ApplicationSignInManager aps, IReportService reportService,IHotelBlockService hotelBlockService)
            : base(userManager, aps)
        {
            this._hotelBlockService = hotelBlockService;
            this._reportService = reportService;
        }

        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult search()
        {

            return View();
        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult getAllHotelBlock(int BlockNum=-100, string BlockName="")
        {
            List<HotelBlockDto> list = _hotelBlockService.GetAll().OrderByDescending(x => x.Id).ToList();
            if (Request.IsAjaxRequest())
            {
                if (BlockNum!=-100)
                {
                    list = list.Where(x => x.BlockNum == BlockNum.ToString()).ToList();
                }
                if (!string.IsNullOrWhiteSpace(BlockName))
                {
                    list = list.Where(x => x.BlockName.Contains(BlockName)).ToList();
                }
            }
            return View(list);
        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        //public ActionResult statuseReport(int? id)
        //{

        //    PercentDto dt = _reportService.getAllRoomsStatusePercentInHotelBlock(id.Value);
        //    return View(dt);
        //}
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult searchForRoomStatus()
        {

            return View();
        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult getAllHotelBlockForRoom(int BlockNum = -100, string BlockName = "")
        {
            List<HotelBlockDto> list = _hotelBlockService.GetAll().OrderByDescending(x => x.Id).ToList();
            if (Request.IsAjaxRequest())
            {
                if (BlockNum != -100)
                {
                    list = list.Where(x => x.BlockNum == BlockNum.ToString()).ToList();
                }
                if (!string.IsNullOrWhiteSpace(BlockName))
                {
                    list = list.Where(x => x.BlockName.Contains(BlockName)).ToList();
                }
            }
            return View(list);
        }

        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult searchForRoom(int?id)
        {
            if (id.HasValue)
            {
                ViewBag.id = id;
                return View();
            }
            else
            {
                return RedirectToAction("searchForRoomStatus");
            }
         
        }

        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult getAllRoom(int roomNum = -100, string roomDesc = "",string Status = "",int? id=-100)
        {
            List<RoomDto> list = _reportService.getAllRoomsWithStatusInHotelBlock(id.Value).OrderByDescending(x => x.Id).ToList();
            if (Request.IsAjaxRequest())
            {
                if (roomNum != -100)
                {
                    list = list.Where(x => x.RoomNum == roomNum.ToString()).ToList();
                }
                
                if (!string.IsNullOrWhiteSpace(roomDesc))
                {
                    list = list.Where(x => x.RoomDirection.Contains(roomDesc)).ToList();
                }
            }
            return View(list);
        }

    }
}