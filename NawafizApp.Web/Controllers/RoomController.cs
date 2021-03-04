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
            var typeMsg = "";
            var blockMsg = "";
            var types = _roomTypeService.GetRoomTypes();
            var hotelBlocks = _hotelBlockService.GetAll();
            typeMsg = !types.Any() ? "يجب إضافة نوع غرفة" :"";
            blockMsg = !hotelBlocks.Any() ? "يجب إضافة كتلة بنائية " : "";
            ViewBag.blockMsg = blockMsg;
            ViewBag.typeMsg = typeMsg;
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

        public ActionResult getAllRoom(string sortOrder, string selectedBlock = "", string selectedType = "", string selectedRoomNum = "")
        {
            var roomsStatusChangedCount = 0;
            List<Helper.MySQlRoom> res=   MysqlFetchingRoomData.getDataFromMySql();
            foreach (var item in res)
            {
                var room = _roomService.GetAll().Where(x => x.RoomNum == item.RoomNum).FirstOrDefault();
                if (room != null)
                {
                    room.IsNeedfix = item.MantStatus == "M" ? true : false;
                    room.isneedclean = item.CleanStatus == "D" ? true : false;
                    if (room.IsNeedfix && !room.Isrequistedfix)
                        roomsStatusChangedCount++;
                    if (room.isneedclean && !room.Isrequisted)
                        roomsStatusChangedCount++;
                    _roomService.Edit(room);
                }

            }
            



            List<RoomDto> list1 = _roomService.GetAll();
            List<RoomDto> list2 = new List<RoomDto>();
            foreach (var item in list1)
            {

                item.TypeName = _roomTypeService.getbyidGetById(item.RoomType_id).Name.ToString();
                item.blockName = _hotelBlockService.GetById(item.HotelBlock_id).BlockName.ToString();
                list2.Add(item);
            }

            var RF = new RoomFilter();
            RF.HotelBlocks = _hotelBlockService.GetAll().Select(x=> new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.BlockName
            });
            RF.Types = _roomTypeService.GetRoomTypes().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var data = list2;
            roomsStatusChangedCount = data.Where(x => (x.isneedclean && !x.Isrequisted) || x.IsNeedfix && !x.Isrequistedfix).Count();
            System.Web.HttpContext.Current.Session["roomsStatusChangedCount"] = roomsStatusChangedCount;
            SignalHelper.SendRoomsStatusChangedCount(roomsStatusChangedCount);
            var selectedTypeValue = 0;
            var isInt = int.TryParse(selectedBlock, out selectedTypeValue);
            if (!string.IsNullOrEmpty(selectedBlock) && isInt)
            {
                data = data.Where(f => f.HotelBlock_id == (int) int.Parse(selectedBlock)).ToList();
            }
            if (!string.IsNullOrEmpty(selectedRoomNum))
            {
                data = data.Where(f => f.RoomNum == selectedRoomNum).ToList();
            }
            selectedTypeValue = 0;
            isInt = int.TryParse(selectedType, out selectedTypeValue);
            if (!string.IsNullOrEmpty(selectedType) && isInt)
            {
                data = data.Where(f => f.RoomType_id == (int)int.Parse(selectedType)).ToList();
            }
            ViewBag.RoomNumberSortParm = !String.IsNullOrEmpty(sortOrder) ? "number_desc" : "number";
            ViewBag.RoomDirSortParm = !String.IsNullOrEmpty(sortOrder) ? "dir_desc" : "dir";
            ViewBag.RoomBlockSortParm = !String.IsNullOrEmpty(sortOrder) ? "block_desc" : "block";
            ViewBag.RoomTypeSortParm = !String.IsNullOrEmpty(sortOrder) ? "type_desc" : "type";
            ViewBag.RoomIsBusySortParm = !String.IsNullOrEmpty(sortOrder) ? "busy_desc" : "busy";
            ViewBag.RoomIsNeedFixSortParm = !String.IsNullOrEmpty(sortOrder) ? "fix_desc" : "fix";
            ViewBag.RoomIsNeedCleanSortParm = !String.IsNullOrEmpty(sortOrder) ? "clean_desc" : "clean";
            ViewBag.RoomIsInServiceSortParm = !String.IsNullOrEmpty(sortOrder) ? "service_desc" : "service";
            switch (sortOrder)
            {
                case "number_desc":
                    data = data.OrderByDescending(s => s.RoomNum).ToList();
                    break;
                case "dir_desc":
                    data = data.OrderByDescending(s => s.RoomDirection).ToList();
                    break;
                case "block_desc":
                    data = data.OrderByDescending(s => s.blockName).ToList();
                    break;
                case "type_desc":
                    data = data.OrderByDescending(s => s.TypeName).ToList();
                    break;
                case "busy_desc":
                    data = data.OrderByDescending(s => s.Isbusy).ToList();
                    break;
                case "fix_desc":
                    data = data.OrderByDescending(s => s.IsNeedfix).ToList();
                    break;
                case "clean_desc":
                    data = data.OrderByDescending(s => s.isneedclean).ToList();
                    break;
                case "service_desc":
                    data = data.OrderByDescending(s => s.IsInService).ToList();
                    break;
                case "number":
                    data = data.OrderBy(s => s.RoomNum).ToList();
                    break;
                case "dir":
                    data = data.OrderBy(s => s.RoomDirection).ToList();
                    break;
                case "block":
                    data = data.OrderBy(s => s.blockName).ToList();
                    break;
                case "type":
                    data = data.OrderBy(s => s.TypeName).ToList();
                    break;
                case "busy":
                    data = data.OrderBy(s => s.Isbusy).ToList();
                    break;
                case "fix":
                    data = data.OrderBy(s => s.IsNeedfix).ToList();
                    break;
                case "clean":
                    data = data.OrderBy(s => s.isneedclean).ToList();
                    break;
                case "service":
                    data = data.OrderBy(s => s.IsInService).ToList();
                    break;
            }
            RF.Data = data.ToList();

            return View(RF);

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
        public ActionResult Edit(RoomDto roomDto, int hid, int tid)
        {

            if (ModelState.IsValid)
            {
                roomDto.RoomType_id = tid;
                roomDto.HotelBlock_id = hid;
                _roomService.Edit(roomDto, true);

                roomrecDto roomrecDto = new roomrecDto();
                roomrecDto.Room_Id =roomDto.Id;
                roomrecDto.Recoed = "تم تعديل  الغرفة ";
                _RoomRecServices.Add(roomrecDto);
                return RedirectToAction("getAllRoom", "Room");

            }
            return View(roomDto);
        }
    }
    public class RoomFilter
    {
        public RoomFilter()
        {
            this.Data = new List<RoomDto>();
        }
        public IList<RoomDto> Data { set; get; }
        public IEnumerable<SelectListItem> HotelBlocks { set; get; }
        public IEnumerable<SelectListItem> Types { set; get; }
        public int SelectedBlock { set; get; }
        public string SelectedRoomNum { set; get; }
        public int SelectedType { set; get; }
    }
}