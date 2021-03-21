using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawafizApp.Web.Controllers
{
    public class DashboardController : Controller
    {
        IFixOrderServices _fixOrderService;
        ICleanOrderService _orderService;
        IRoomService _roomService;
        public DashboardController(IFixOrderServices fixOrderService, ICleanOrderService orderService,IRoomService roomService)
        {
            _roomService = roomService;
            _fixOrderService = fixOrderService;
            _orderService = orderService;
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public double GetNotFinishedPercentageForCleaner()
        {
            var allClean = _orderService.GetAll();
            var percentage = (allClean.Where(x => x.isFinished).Count() * 100) / allClean.Count();
            return percentage;
        }
        [HttpGet]
        public double GetNotFinishedPercentageForFixer()
        {
            var allFixOrders = _fixOrderService.GetAll();
            var percentage = (allFixOrders.Where(x => x.isFinished).Count() * 100) / allFixOrders.Count();
            return percentage;
        }

        public double GetCleaningRoomPercentage()
        {
            var allroom = _roomService.GetAll();
            var percentage = (allroom.Where(x => x.isneedclean).Count() * 100) / allroom.Count();
            return percentage;
        }

        public double GetFixRoomPercentage()
        {
            var allroom = _roomService.GetAll();
            var percentage = (allroom.Where(x => x.IsNeedfix).Count() * 100) / allroom.Count();
            return percentage;
        }
    }
}