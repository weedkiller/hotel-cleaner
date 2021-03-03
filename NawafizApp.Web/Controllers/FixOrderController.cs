using Microsoft.AspNet.Identity;
using NawafizApp.Common;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using NawafizApp.Services.Interfaces;
using NawafizApp.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawafizApp.Web.Controllers
{
    public class FixOrderController : BaseAuthorizeController
    {
        IUserService _userService;
        IEquipmentService _equipmentService;

        IRoomService _roomService;
        IFixOrderServices _fixOrderServices;
        IFixOrderEqupService _fixOrderEqupService;

        public FixOrderController(ApplicationUserManager userManager, ApplicationSignInManager aps, IUserService IUS, IEquipmentService equipmentService, IRoomService roomService, IFixOrderServices fixOrderServices, IFixOrderEqupService fixOrderEqupService) : base(userManager, aps)
        {
            _roomService = roomService;
            this._userService = IUS;
            this._equipmentService = equipmentService;
            _fixOrderServices = fixOrderServices;
            _fixOrderEqupService = fixOrderEqupService;
        }
        // GET: FixOrder
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult AddFixOrder()
        {
            return View();
        }
        [HttpPost]

        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult AddFixOrder(FixOrderDto fixOrderDto,int Rid )
        {

            fixOrderDto.Room_ID = Rid;
            fixOrderDto.Hoster = Guid.Parse(User.Identity.GetUserId());
            fixOrderDto.moshId = _fixOrderServices.getmoshbyroomId((int)Rid);
            fixOrderDto.Creation_At = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY) + " " + DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            var room = _roomService.GetById(Rid);
            room.Isrequistedfix = true;
            _roomService.Edit(room);
            var fixorder_id = _fixOrderServices.addFixOrder(fixOrderDto);
            var equ = _equipmentService.All(Convert.ToInt32(Rid));

            foreach (var item in equ)
            {
                if (item.needfix == true)
                {
                    FixOrderEqupDto fixOrderEqupDto = new FixOrderEqupDto();
                    fixOrderEqupDto.Name = item.Name;
                    fixOrderEqupDto.FixOrder = fixorder_id;
                    _fixOrderEqupService.add(fixOrderEqupDto, fixorder_id);
                }

            }

            return RedirectToAction("RoomView", "Room");
        }

        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult GetAllFixOrder()
        {
            List<FixOrderDto> list1 = new List<FixOrderDto>();
            var finishedCount = 0;
            var allCount = 0;
            var dc = _fixOrderServices.GetAll().OrderByDescending(x => x.Id);
            foreach (var item in dc)
            {
                item.moshrefname = _userService.GetById((Guid)item.moshId).FullName.ToString();
                item.HosterName = _userService.GetById((Guid)item.Hoster).FullName.ToString();
                if (item.maitremp == null)
                {
                    item.empName = "لم يتم أرسالها الى الموظف بعد ";
                }
                else
                {
                    item.empName = _userService.GetById((Guid)item.maitremp).FullName.ToString();
                }
                item.Roomnu = _roomService.GetById((int)item.Room_ID).RoomNum.ToString();
                if (item.isFinished)
                    finishedCount++;
                list1.Add(item);
                allCount++;
            }


            ViewBag.FixFinishedCount = ((finishedCount * 100) / allCount) + " %";
            return View(list1);

        }
        public ActionResult Getallformosh()
        {


            _fixOrderServices.setIsSeenTrueForMosherf();
            List<FixOrderDto> list1 = new List<FixOrderDto>();

            List<FixOrderDto> list = _fixOrderServices.GetAll().OrderByDescending(x => x.Id).Where(x => x.moshId == new Guid(User.Identity.GetUserId())).Where(x => x.isFinished == false).ToList();
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
                if (item.maitremp == null)
                {
                    item.empName = "لم يتم أرسالها الى موظف الصيانة ";
                }
                else
                {
                    item.empName = _userService.GetById((Guid)item.maitremp).FullName.ToString();
                }

                item.Roomnu = _roomService.GetById((int)item.Room_ID).RoomNum.ToString();
                list1.Add(item);
            }

            if (Request.IsAjaxRequest())
            {




            }
            return View(list1);
        }
        public ActionResult sendOrderToemp(int id)
        {

            return View(_fixOrderServices.GetById(id));


        }
        [HttpPost]
        public ActionResult sendOrderToemp(FixOrderDto dto, string eid)
        {



            if (ModelState.IsValid)
            {

                dto = _fixOrderServices.GetById(dto.Id);
                dto.maitremp = _userService.GetById(new Guid(eid)).UserId;

                _fixOrderServices.edit(dto);



            }
            return RedirectToAction("Getallformosh");



        }


        public ActionResult GetallforCleanEmp()
        {

            _fixOrderServices.setIsSeenTrue();
            List<FixOrderDto> list1 = new List<FixOrderDto>();

            var dc = _fixOrderServices.GetAll().OrderByDescending(x => x.Id).Where(x => x.maitremp == new Guid(User.Identity.GetUserId())).Where(x => x.isFinished == false);
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
                if (item.maitremp == null)
                {
                    item.empName = "لم يتم أرسالها الى موظف التنظيف";
                }
                else
                {
                    item.empName = _userService.GetById((Guid)item.maitremp).FullName.ToString();
                }
                list1.Add(item);

            }


            return View(list1);
        }


        public ActionResult takeCleanOrder(int id)
        {
            var dto = _fixOrderServices.GetById(id);
            dto.moshrefname = _userService.GetById((Guid)dto.moshId).FullName.ToString();
            dto.HosterName = _userService.GetById((Guid)dto.Hoster).FullName.ToString();
            dto.empName = _userService.GetById((Guid)dto.maitremp).FullName.ToString();
            dto.Roomnu = _roomService.GetById((int)dto.Room_ID).RoomNum.ToString();
            return View(dto);

        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]

        [HttpPost]
        public ActionResult takeCleanOrder(CleanOrderDto cleanOrderDto)
        {
            var dto = _fixOrderServices.GetById(cleanOrderDto.Id);
            dto.startdate = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY) + " " + DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            var id = User.Identity.GetUserId();
            var guid = new Guid(id);
            var userdto = _userService.GetById(guid);
            dto.Istaked = true;
            userdto.IsBusy = true;
            dto.isFinished = false;
            _userService.Edit(userdto, guid);
            _fixOrderServices.edit(dto);
            TempData["ord"] = dto.Id;
            return RedirectToAction("EndCleanOrder", new { rid = dto.Room_ID,oid = dto.Id });

        }


        public ActionResult EndCleanOrder(int rid)
        {

            var dto = _equipmentService.All(rid).Where(x=>x.ishere==true & x.needfix==true);
            return View(dto);

        }
        [HttpPost]
        public ActionResult EndCleanOrder(int rid,int oid)
        {
            var cleanOrderDto = _fixOrderServices.GetById(oid);
            var userdto = _userService.GetById(new Guid(User.Identity.GetUserId()));
            userdto.IsBusy = false;
            _userService.Edit(userdto, new Guid(User.Identity.GetUserId()));
            cleanOrderDto.enddate = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY) + " " + DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            cleanOrderDto.isFinished = true;
            _fixOrderServices.edit(cleanOrderDto);

            var rom = _roomService.GetById(Convert.ToInt32(cleanOrderDto.Room_ID));
            //rom. = false;
            rom.Isrequistedfix = false;
            rom.IsNeedfix = false;
            _roomService.Edit(rom);

            MysqlFetchingRoomData.SetFixStatus(rom.RoomNum, rom.IsNeedfix);
            return RedirectToAction("GetallforCleanEmp");

        }


        public ActionResult CheckedToggle(int id, int rid)
        {
            _equipmentService.checkedToggle(id);
            var room = _roomService.GetById(rid);
            room.IsNeedfix = !room.IsNeedfix;
            _roomService.Edit(room);

            MysqlFetchingRoomData.SetFixStatus(room.RoomNum, room.IsNeedfix);
            return RedirectToAction("EndCleanOrder");



        }

        public ActionResult CheckedToggleFix(int id, int Rid)
        {
            _equipmentService.checkedToggleFix(id);
            var room = _roomService.GetById(Rid);
            room.IsNeedfix = !room.IsNeedfix;
            _roomService.Edit(room);

            MysqlFetchingRoomData.SetFixStatus(room.RoomNum, room.IsNeedfix);
            return RedirectToAction("EndCleanOrder", new { Rid = Rid });



        }

        public ActionResult EndCleancOrder(int id)
        {
            var dto = _fixOrderServices.GetById(id);
            dto.empName = _userService.GetById((Guid)dto.maitremp).FullName.ToString();
            return View(dto);

        }
        [HttpPost]
        public ActionResult EndClxeanOrder(FixOrderDto cleanOrderDto)
        {
            var userdto = _userService.GetById(new Guid(User.Identity.GetUserId()));
            userdto.IsBusy = false;
            _userService.Edit(userdto, new Guid(User.Identity.GetUserId()));
            cleanOrderDto.enddate = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY) + " " + DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            cleanOrderDto.isFinished = true;
            _fixOrderServices.edit(cleanOrderDto);

            var rom = _roomService.GetById(Convert.ToInt32(cleanOrderDto.Room_ID));
            //rom. = false;
            rom.Isrequistedfix = false;
            _roomService.Edit(rom);
            return RedirectToAction("Check", "Equipment", rom.Id);

        }


        public ActionResult notfinishedOrderforCurentUser()
        {
            List<FixOrderDto> list1 = new List<FixOrderDto>();
            var dto = _fixOrderServices.GetAll().Where(x => x.Istaked == true).Where(x => x.isFinished == false).Where(x => x.maitremp == new Guid(User.Identity.GetUserId()));
            foreach (var item in dto)
            {
                item.moshrefname = _userService.GetById((Guid)item.moshId).FullName.ToString();
                item.HosterName = _userService.GetById((Guid)item.Hoster).FullName.ToString();
                item.empName = _userService.GetById((Guid)item.maitremp).FullName.ToString();
                item.Roomnu = _roomService.GetById((int)item.Room_ID).RoomNum.ToString();
                list1.Add(item);

            }


            return View(dto);

        }



        [HttpPost]

        public ActionResult EndCleanOrder1(int id)
        {
            var cleanOrderDto = _fixOrderServices.GetById(id);
            var userdto = _userService.GetById(new Guid(User.Identity.GetUserId()));
            userdto.IsBusy = false;
            _userService.Edit(userdto, new Guid(User.Identity.GetUserId()));
            cleanOrderDto.enddate = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY) + " " + DateTimeHelper.ConvertTimeToString(Utils.ServerNow.TimeOfDay, TimeFormats.HH_MM_AM);
            cleanOrderDto.isFinished = true;
            _fixOrderServices.edit(cleanOrderDto);

            var rom = _roomService.GetById(Convert.ToInt32(cleanOrderDto.Room_ID));
            rom.IsNeedfix = false;
            rom.Isrequistedfix = false;
            _roomService.Edit(rom);

            MysqlFetchingRoomData.SetFixStatus(rom.RoomNum, rom.IsNeedfix);
            return RedirectToAction("Check", "Equipment", rom.Id);

        }

        public ActionResult getequpment(int oid)
        {
            return View(_fixOrderEqupService.All(oid));
        }





    }
}