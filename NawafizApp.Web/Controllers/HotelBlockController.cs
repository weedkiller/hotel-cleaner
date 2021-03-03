using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using NawafizApp.Services.Interfaces;
using NawafizApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawafizApp.Web.Controllers
{
    public class HotelBlockController : BaseAuthorizeController
    {
        IHotelBlockService _IHotelBlockService;
        IUserService _userservice;
        public HotelBlockController(ApplicationUserManager userManager, ApplicationSignInManager aps, IHotelBlockService IHotelBlockService,IUserService userService)
            : base(userManager, aps)
        {
            _userservice = userService;
            this._IHotelBlockService = IHotelBlockService;
        }

        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult AddHotelBlock()
        {
            //ViewBag.url = "~/Uploads/Lighthouse.jpg";
            return View();
        }

        [HttpPost]


        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult AddHotelBlock(HotelBlockDto dto, List<string> sids, List<string> mgId, List<DateTime> fromTime, List<DateTime> toTime, List<string> cids,List<string> mids,List<string> rids)
        {
            List<string> Ids = new List<string>();
            if (mgId != null)
            {
                var index = 0;
                foreach (var item in mgId)
                {
                    Ids.Add(item);
                    dto.Supervisors.Add(new SupervisorDto()
                    {
                        Id = item,
                        FromTime = fromTime[index],
                        ToTime = toTime[index],
                    });
                    index++;
                }
            }
            if (sids != null)
            {
                foreach (var item in sids)
                {
                    Ids.Add(item);
                }
            }
            if (cids != null)
            {
                foreach (var item in cids)
                {
                    Ids.Add(item);
                }
            }
            if (mids != null)
            {
                foreach (var item in mids)
                {
                    Ids.Add(item);
                }
            }
            if (rids != null)
            {
                foreach (var item in rids)
                {
                    Ids.Add(item);
                }
            }
            dto.Ids = Ids;
            int i = _IHotelBlockService.addHotelBlock(dto);
            return RedirectToAction("getHotelBlocks");
        }
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public JsonResult GetSupervisors()
        {
            var list = Selects.ManagerEmpUsers(null);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //[Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        [Authorize(Roles = "Admin,Hoster")]
        public ActionResult getHotelBlocks()
        {
            var result = _IHotelBlockService.GetAll().OrderByDescending(x => x.Id);
            return View(result);
        }

        public ActionResult getblockemp(int id)
        {
            var result = _userservice.usersforblock(id);
            return View(result);
        }


        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult Edit(int id)
        {

            var dto = _IHotelBlockService.GetById(id);
            ViewBag.Values = dto.Ids;
            return View(dto);
        }
        [HttpPost]

        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult Edit(HotelBlockDto dto, List<string> sids, List<string> mgId, List<string> cids, List<string> mids, List<string> rids)
        {

            if (ModelState.IsValid)
            {
                List<string> Ids = new List<string>();
                if (mgId!=null)
                {
                    foreach (var item in mgId)
                    {
                        Ids.Add(item);
                    }
                }
                if (sids != null)
                {
                    foreach (var item in sids)
                    {
                        Ids.Add(item);
                    }
                }
                if (cids != null)
                {
                    foreach (var item in cids)
                    {
                        Ids.Add(item);
                    }
                }
                if (mids!=null)
                {
                    foreach (var item in mids)
                    {
                        Ids.Add(item);
                    }
                }
                if (rids!=null)
                {
                    foreach (var item in rids)
                    {
                        Ids.Add(item);
                    }
                }
              
                dto.Ids = Ids;
                

                _IHotelBlockService.edit(dto);
                return RedirectToAction("getHotelBlocks", "HotelBlock");

            }
            return View(dto);
        }


        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult deleteHotelBlock(int id)
        {
            var x = _IHotelBlockService.delete(id);
            if (x)
                return RedirectToAction("getHotelBlocks");
            return RedirectToAction("Error");
        }

    }
}