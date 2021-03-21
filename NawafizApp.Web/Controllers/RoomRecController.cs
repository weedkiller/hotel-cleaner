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
    public class RoomRecController : Controller
    {

        public IRoomRecServices RoomRecServices { get; }
        public ApplicationUserManager UserManager { get; }
        public ApplicationSignInManager Aps { get; }
        public IUserService IUS { get; }
        public IRoomService RoomService { get; }
        public IRoomTypeService RoomTypeService { get; }
        public IHotelBlockService HotelBlockService { get; }
        // GET: RoomRec
        public RoomRecController(IRoomRecServices RoomRecServices, ApplicationUserManager userManager, ApplicationSignInManager aps, IUserService IUS, IRoomService roomService, IRoomTypeService roomTypeService, IHotelBlockService hotelBlockService)
        {
            this.RoomRecServices = RoomRecServices;
            UserManager = userManager;
            Aps = aps;
            this.IUS = IUS;
            RoomService = roomService;
            RoomTypeService = roomTypeService;
            HotelBlockService = hotelBlockService;
        }
        public ActionResult getallrec()
        {
            return View(RoomRecServices.getall());
        }




    }
}