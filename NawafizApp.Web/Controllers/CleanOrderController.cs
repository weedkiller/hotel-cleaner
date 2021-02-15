using Microsoft.AspNet.Identity;
using NawafizApp.Common;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using NawafizApp.Services.Interfaces;
using NawafizApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NawafizApp.Web.Controllers
{
    public class CleanOrderController : BaseAuthorizeController
    {
        ICleanOrderService _orderService;
        INotifictationService _notifictationService;
        IRoomService _RoomService;
        IUserService _userService;
        public CleanOrderController( ApplicationUserManager userManager, ApplicationSignInManager aps, IRoomService RoomService, IUserService userService, INotifictationService notifictationService, ICleanOrderService orderService)
            : base(userManager, aps)
        {
            _userService=userService;
            this._orderService = orderService;
            this._RoomService = RoomService;
            this._notifictationService = notifictationService;

        }
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult AddOrder()
        {
           
            
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult AddOrder(CleanOrderDto dto,  int rid)
        {
            
            dto.Hoster = Guid.Parse(User.Identity.GetUserId());
            dto.Creation_Date = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY);
            dto.Creation_Time = DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            dto.Creation_At = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY)+" " + DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            dto.Room_ID = rid;
            dto.moshId = _orderService.getmoshbyroomId(rid);
            int i = _orderService.addOrder(dto);
            var room = _RoomService.GetById(rid);
            room.Isrequisted = true;
            _RoomService.Edit(room);
            //NotifictationDto dto1 = new NotifictationDto();
            //dto1.senderId = Guid.Parse(User.Identity.GetUserId());
            //dto1.RevieverId = _RoomService.getMangerIdForRoom(Convert.ToInt32( rid));
            //var roomnum = _RoomService.GetById(dto.Room_ID.Value).RoomNum;
            
            //var s1 = "الغرفة رقم";
            //var s2 = roomnum;
            //var s3 = " بحاجة الى ";
            //var s4 = "&";
            //var s5 = i;
            //var s6 = "تنظيف";
           
            //dto1.NotDateTime = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY);
            //dto1.Room_ID = dto.Room_ID;
            //_notifictationService.Add(dto1);
            return RedirectToAction("getAllRoom","Room");
        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]


        public ActionResult getAllOrders()
        {
            List<CleanOrderDto> list1 = new List<CleanOrderDto>();
            List<CleanOrderDto> list = _orderService.GetAll().OrderByDescending(x => x.Id).ToList();
            foreach (var item in list)
            {

                if (item.moshId == null   )
                { 
                    item.moshrefname = "لم يتم أرسالها الى المشرف ";
                }
                else
                {
                    item.moshrefname = _userService.GetById((Guid)item.moshId).FullName.ToString(); 
                }
                if   (item.Hoster==null)
                { 
                    item.HosterName = "لم تنشأ من موظف الحجز....! ";
                
                }
                else
                {
                    item.HosterName = _userService.GetById((Guid)item.Hoster).FullName.ToString();
                }
                if (item.cleaningEmp==null)
                {
                    item.empName = "لم يتم أرسالها الى موظف التنظيف";
                }
                else
                { 
                    item.empName = _userService.GetById((Guid)item.cleaningEmp).FullName.ToString();
                }
                
                item.Roomnu = _RoomService.GetById((int)item.Room_ID).RoomNum.ToString();
                list1.Add(item);
            }


                return View(list1);

           

        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult sendOrderToemp(int id)
        {

            return View(_orderService.GetById(id));
        
        
        }


        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]

        [HttpPost]
        public ActionResult sendOrderToemp(CleanOrderDto dto,string eid)
        {

            

            if (ModelState.IsValid)
            {
                
                dto = _orderService.GetById(dto.Id);
                dto.cleaningEmp = _userService.GetById(new Guid(eid)).UserId;

                _orderService.edit(dto);



            }
            return RedirectToAction("Getallformosh");

        }

        //[Authorize(Roles = "Admin,manager")]
        public ActionResult Getallformosh()
        {


            List<CleanOrderDto> list1 = new List<CleanOrderDto>();

            List<CleanOrderDto> list = _orderService.GetAll().OrderByDescending(x => x.Id).Where(x=>x.moshId== new Guid( User.Identity.GetUserId())).Where(x=>x.isFinished==false).ToList();
            foreach (var item in list)
            {

                if (item.moshId == null)
                {
                    item.moshrefname = "لم يتم أرسالها الى المشرف ";
                }
                else
                {
                    item.moshrefname = _userService.GetById((Guid)item.moshId).FullName.ToString();
                }
                if (item.Hoster == null)
                {
                    item.HosterName = "لم تنشأ من موظف الحجز....! ";

                }
                else
                {
                    item.HosterName = _userService.GetById((Guid)item.Hoster).FullName.ToString();
                }
                if (item.cleaningEmp == null)
                {
                    item.empName = "لم يتم أرسالها الى موظف التنظيف";
                }
                else
                {
                    item.empName = _userService.GetById((Guid)item.cleaningEmp).FullName.ToString();
                }

                item.Roomnu = _RoomService.GetById((int)item.Room_ID).RoomNum.ToString();
                list1.Add(item);
            }

            if (Request.IsAjaxRequest())
            {



 
            }
            return View(list1);
        }

        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult deleteOrder(int id)
        {
            var x = _orderService.delete(id);
            if (x)
                return RedirectToAction("AddOrder");
            return RedirectToAction("Error");
        }
        [Authorize(Roles = "Admin,ReservationEmp,manager")]

        public ActionResult Edit(int id)
        {
         
            var dto = _orderService.GetById(id);
            return View(dto);
        }
        [HttpPost]
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]

        public ActionResult Edit(CleanOrderDto orderDto, Guid eid)
        {


                orderDto.cleaningEmp = eid;
                _orderService.edit(orderDto);
                return RedirectToAction("Getallformosh", "CleanOrder");

           
            
        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]

        public ActionResult GetallforCleanEmp()
        {
               List<CleanOrderDto> list1 = new List<CleanOrderDto>();

            var dc = _orderService.GetAll().OrderByDescending(x => x.Id).Where(x => x.cleaningEmp == new Guid(User.Identity.GetUserId())).Where(x => x.isFinished == false);
            foreach (var item in dc)
            {

                if (item.moshId == null)
                {
                    item.moshrefname = "لم يتم أرسالها الى المشرف ";
                }
                else
                {
                    item.moshrefname = _userService.GetById((Guid)item.moshId).FullName.ToString();
                }
                if (item.Hoster == null)
                {
                    item.HosterName = "لم تنشأ من موظف الحجز....! ";

                }
                else
                {
                    item.HosterName = _userService.GetById((Guid)item.Hoster).FullName.ToString();
                }
                if (item.cleaningEmp == null)
                {
                    item.empName = "لم يتم أرسالها الى موظف التنظيف";
                }
                else
                {
                    item.empName = _userService.GetById((Guid)item.cleaningEmp).FullName.ToString();
                }
                list1.Add(item);

            }

            
            return View(list1);
        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]

        public ActionResult takeCleanOrder(int id)
        {
            var dto = _orderService.GetById(id);
            dto.moshrefname = _userService.GetById((Guid)dto.moshId).FullName.ToString();
            dto.HosterName = _userService.GetById((Guid)dto.Hoster).FullName.ToString();
            dto.empName= _userService.GetById((Guid)dto.cleaningEmp).FullName.ToString();
            dto.Roomnu = _RoomService.GetById((int)dto.Room_ID).RoomNum.ToString();
            return View(dto);

        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]

        [HttpPost]
        public ActionResult takeCleanOrder(CleanOrderDto cleanOrderDto)
        {
            var dto = _orderService.GetById(cleanOrderDto.Id);
            dto.startdate = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY) + " " + DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            var id = User.Identity.GetUserId();
            var guid = new Guid(id);
            var userdto = _userService.GetById(guid);
            dto.Istaked = true;
            userdto.IsBusy = true;
            dto.isFinished = false;
            _userService.Edit(userdto, guid);
            _orderService.edit(dto);
            TempData["ord"] = dto.Id;
            return RedirectToAction("EndCleanOrder");

        }

        public ActionResult EndCleanOrder()
        {
            var dto = _orderService.GetById(Convert.ToInt32(TempData["ord"]));
            dto.empName = _userService.GetById((Guid)dto.cleaningEmp).FullName.ToString();
            return View(dto);

        }
       

        public ActionResult notfinishedOrderforCurentUser()
        {
            List<CleanOrderDto> list1 = new List<CleanOrderDto>();
            var dto = _orderService.GetAll().Where(x => x.Istaked == true).Where(x=>x.isFinished==false).Where(x=>x.cleaningEmp==new Guid(User.Identity.GetUserId()));
            foreach (var item in dto)
            {
                item.moshrefname = _userService.GetById((Guid)item.moshId).FullName.ToString();
                item.HosterName = _userService.GetById((Guid)item.Hoster).FullName.ToString();
                item.empName = _userService.GetById((Guid)item.cleaningEmp).FullName.ToString();
                item.Roomnu = _RoomService.GetById((int)item.Room_ID).RoomNum.ToString();
                list1.Add(item);

            }


            return View(dto);

        }

        public ActionResult EndCleanOrder1()
        {
            var dto = _orderService.GetById(Convert.ToInt32(TempData["ord"]));
            dto.empName = _userService.GetById((Guid)dto.moshId).FullName.ToString();
            return View(dto);




        }
        [HttpPost]

        public ActionResult EndCleanOrder1(int id)
        {
            var cleanOrderDto = _orderService.GetById(id);
            var userdto = _userService.GetById(new Guid(User.Identity.GetUserId()));
            userdto.IsBusy = false;
            _userService.Edit(userdto, new Guid(User.Identity.GetUserId()));
            cleanOrderDto.enddate = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY) + " " + DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            cleanOrderDto.isFinished = true;
            _orderService.edit(cleanOrderDto);

            var rom = _RoomService.GetById(Convert.ToInt32(cleanOrderDto.Room_ID));
            rom.isneedclean = false;
            rom.Isrequisted = false;
            _RoomService.Edit(rom);
            return RedirectToAction("Check", "Equipment", rom.Id);

        }
        [HttpPost]
        public ActionResult EndCleanOrder(CleanOrderDto cleanOrderDto)
        {
            var userdto = _userService.GetById(new Guid(User.Identity.GetUserId()));
            userdto.IsBusy=false;
            _userService.Edit(userdto, new Guid(User.Identity.GetUserId()));
            cleanOrderDto.enddate = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY) + " " + DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            cleanOrderDto.isFinished = true;
            _orderService.edit(cleanOrderDto);
            
            var rom = _RoomService.GetById(Convert.ToInt32(cleanOrderDto.Room_ID));
            rom.isneedclean = false;
            rom.Isrequisted = false;
            _RoomService.Edit(rom);
            return RedirectToAction("Check", "Equipment",rom.Id);

        }



    }
}