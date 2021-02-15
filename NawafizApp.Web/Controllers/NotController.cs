using Microsoft.AspNet.Identity;
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
    public class NotController : BaseAuthorizeController
    {
        INotifictationService _notifictationService;
        IRoomService _RoomService;
        ICleanOrderService _OrderService;
        IReportService _reportService;

        public NotController(ApplicationUserManager userManager, ApplicationSignInManager aps, IReportService reportService, ICleanOrderService OrderService, IRoomService RoomService, INotifictationService notifictationService)
            : base(userManager, aps)
        {
            this._OrderService = OrderService;
            this._RoomService = RoomService;
            this._notifictationService = notifictationService;
            this._reportService = reportService;
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult NotificationsView()
        {
            return View();
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public PartialViewResult getNotification()
        {

            return PartialView(_notifictationService.All(getGuid(User.Identity.GetUserId())));
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult OpenNotifications(int? notid, int?roomId)
        {
            ViewBag.Id = notid;
            var not = _notifictationService.GetById(notid.Value);
            not.IsRead = true;
            _notifictationService.Edit(not);
            ViewBag.roomId = roomId;
            return View(not);
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult SendNotifications(int? id, int? roomId,string stat="")
        {
            var room = _RoomService.GetById(roomId.Value);
                NotifictationDto dto = new NotifictationDto();
           
              dto.senderId = Guid.Parse(User.Identity.GetUserId());
               dto.RevieverId = _RoomService.getMangerIdForRoom(roomId.Value);
                var s1 = "الغرفة رقم";
                var s2 = room.RoomNum;
            var s3 = " بحاجة الى ";
            var s6 = "تنظيف";
                var s7 = "صيانة";
                if (stat.Contains("صيانة"))
                {

                    dto.NotText = s1 + s2 + s3 + s7 ;

                }
                if (stat.Contains("تنظيف"))
                {

                    dto.NotText = s1 + s2 + s3 + s6 ;
                }
                    dto.NotDateTime = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY);
                    dto.Room_ID = roomId;
                    _notifictationService.Add(dto);
                    dto = new NotifictationDto();
             
            return RedirectToAction("NotificationsView");
        }

        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult AddMaintenanceRequest(int? roomId,int? rromNum)
        {
          
            NotifictationDto dto = new NotifictationDto();

            foreach (var item in _RoomService.getReservationEmpIdForRoom(roomId.Value))
            {
                dto.senderId = Guid.Parse(User.Identity.GetUserId());
                dto.RevieverId = item;
                var s1 = "الغرفة رقم";
                var s2 = rromNum;
                var s3 = " بحاجة الى ";
                var s6 = "تنظيف";
                var s7 = "صيانة";
            

                    dto.NotText = s1 + s2 + s3 + s7;

            
         
                dto.NotDateTime = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY);
                dto.Room_ID = roomId;
                _notifictationService.Add(dto);
                dto = new NotifictationDto();
            }


            return RedirectToAction("searchForRoom");
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult AddCleanRequest(int? roomId, int? rromNum)
        {

            NotifictationDto dto = new NotifictationDto();

            foreach (var item in _RoomService.getReservationEmpIdForRoom(roomId.Value))
            {
                dto.senderId = Guid.Parse(User.Identity.GetUserId());
                dto.RevieverId = item;
                var s1 = "الغرفة رقم";
                var s2 = rromNum;
                var s3 = " بحاجة الى ";
                var s6 = "تنظيف";
                var s7 = "صيانة";


                dto.NotText = s1 + s2 + s3 + s6;



                dto.NotDateTime = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY);
                dto.Room_ID = roomId;
                _notifictationService.Add(dto);
                dto = new NotifictationDto();
            }


            return RedirectToAction("searchForRoom");
        }
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult searchForRoom()
        { 
                return View();
        }

        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult getAllRoom(int roomNum = -100, string roomDesc = "", string Status = "", int? id = -100)
        {
            List<RoomDto> list = _reportService.getAllRooms().OrderByDescending(x => x.Id).ToList();
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
        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        public ActionResult updateOrder(string start, string end,int? id)
        {
            var order = _OrderService.GetById(id.Value);
            order.startdate = start;
            order.enddate = end;
            order.isFinished = true;
            _OrderService.edit(order);
            return RedirectToAction("NotificationsView");
        }
    }
}