using NawafizApp.Services.Dtos;
using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.IO;
using FluentValidation.Mvc;
using NawafizApp.Common;
using Microsoft.AspNet.Identity;

namespace CoreApp.Web.Controllers
{
    public class HomeController : Controller
    {

        IUserService _iuserService;



        public HomeController(IUserService iuserService)
        {

            _iuserService = iuserService;

        }


        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]

        public ActionResult Index()
        {

            //  ViewBag.numOfMains = _ims.getAllStatesAndCategories().mainCategories.Count;
            // ViewBag.numOfSubs = _ims.getAllSubCategories().Count;
            //var rrr = _ims.getAllActivitiesForShow();
            //ViewBag.numOfActs = rrr.Count;
            //ViewBag.numOfActsToday = rrr.Where(y=> y.date.Date == Utils.ServerNow.Date).ToList().Count;
            //ViewBag.numOfUsers = _iuserService.GetAll().Where(q=>!_iuserService.HasRole(q.UserId,"Admin")).ToList().Count;
            //ViewBag.numOfUsersToday = rrr.Where(u=>u.date.Date == Utils.ServerNow.Date && !_iuserService.HasRole(u.UserId,"Admin")).Select(y => y.UserId).Distinct().ToList().Count;
            //ViewBag.usersWorking = rrr.Where(u => u.date.Date == Utils.ServerNow.Date && !_iuserService.HasRole(u.UserId, "Admin")).Select(y => y.UserFullName).Distinct().ToList();

            //rrr.Reverse();
            //rrr=  rrr.Take(9).ToList();


            //ViewBag.last4 = rrr;
            /*HouseKeep
Reception
Admin
Hoster
service
MaintenanceEmp
BlockSupervisor
Cleaner
            */
            var ok = _iuserService.GetById(new Guid(User.Identity.GetUserId())).role;
            switch (ok)
            {
                case "Cleaner":
                    return RedirectToAction("GetallforCleanEmp", "CleanOrder");

                case "Admin":
                    return RedirectToAction("getHotelBlocks", "HotelBlock");

                case "BlockSupervisor":
                    return RedirectToAction("Index", "Dashboard");
                    
                case "Hoster":
                    return RedirectToAction("getAllRoom", "Room");
                case "MaintenanceEmp":
                    return RedirectToAction("GetallforCleanEmp", "FixOrder");
                case "Reception":
                    return RedirectToAction("getallformEmp", "CleanOrder");
                default:
                    return RedirectToAction("AddHotelBlock", "CleanOrder");
                    
            }

        }



    



        




        public ActionResult blank()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}