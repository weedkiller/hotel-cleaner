using Microsoft.AspNet.Identity;
using NawafizApp.Common;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using NawafizApp.Services.Interfaces;
using NawafizApp.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NawafizApp.Web.Controllers
{
    public class ImportFromExcelController : BaseAuthorizeController
    {
        INotifictationService _notifictationService;
        IRoomService _roomService;
        IRoomStatusService _roomStatusService;
        IUserService _userService;
        public ImportFromExcelController(ApplicationUserManager userManager, ApplicationSignInManager aps, IUserService _userService, IRoomStatusService roomStatusService, IUserService IUS, INotifictationService notifictationService, IRoomService roomService)
         : base(userManager, aps)
        {
            this._notifictationService = notifictationService;
            this._roomService = roomService;
            this._roomStatusService = roomStatusService;
        }

        [Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]

        public ActionResult importFromExcel()
        {


            return View();


        }
        //[HttpPost]
        //[Authorize(Roles = "Admin,ReservationEmp,HouseKeepingEmp,ReceptiomEmp,service,manager,MaintenanceEmp,CleanEmp")]
        //[ValidateAntiForgeryToken]
        //    public ActionResult importFromExcel(HttpPostedFileBase postedFile)
        //    {
        //        string uploadPath = System.Configuration.ConfigurationManager.AppSettings["uploadPath"];
        //        string ext = "";
        //        if (postedFile != null)
        //        {
        //            //Extract Image File Name.
        //            string fileName1 = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
        //            ext = System.IO.Path.GetExtension(postedFile.FileName);
        //            fileName1 = fileName1 + Guid.NewGuid();
        //            //Set the Image File Path.
        //            string filePath = uploadPath + fileName1 + ext;

        //            //Save the Image File in Folder.
        //            postedFile.SaveAs(Server.MapPath(filePath));
        //            var fname = fileName1 + ext;

        //            var reslpath = Server.MapPath(filePath);

        //            string sourceFile = reslpath;
        //            string destinationFile =Server.MapPath(uploadPath+ "real.xlsx)");




        //            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
        //            app.DisplayAlerts = false;
        //            // Open Excel Workbook for conversion.
        //            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = app.Workbooks.Open(sourceFile);
        //            // Save file as CSV file.

        //            excelWorkbook.SaveAs(destinationFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSVWindows);
        //            // Close the Workbook.
        //            excelWorkbook.Close();
        //            // Quit Excel Application.
        //            app.Quit();
        //            var lines =System.IO.File.ReadAllLines(destinationFile, Encoding.Default);

        //            string valueNum = "";
        //            string valueStat = "";

        //            foreach (var item in lines)
        //            {


        //                valueNum =item.Split(',')[0];
        //                var room = _roomService.GetAll().Where(x => x.RoomNum == valueNum).SingleOrDefault();
        //                valueStat= item.Split(',')[3];
        //                if (room!=null)
        //                {

        //                var RoomStatus_ID = 0;
        //                if (valueStat.Contains("تنظيف"))
        //                {
        //                   RoomStatus_ID = _roomStatusService.All(room.Id).Where(x => x.Status.Contains("تنظيف")).SingleOrDefault().Id;
        //                }
        //                if (valueStat.Contains("صيانة"))
        //                {
        //                    RoomStatus_ID = _roomStatusService.All(room.Id).Where(x => x.Status.Contains("صيانة")).SingleOrDefault().Id;
        //                }
        //                if (valueStat.Contains("فارغة"))
        //                {
        //                    RoomStatus_ID = _roomStatusService.All(room.Id).Where(x => x.Status.Contains("فارغة")).SingleOrDefault().Id;
        //                }
        //                if (valueStat.Contains("مشغولة"))
        //                {
        //                    RoomStatus_ID = _roomStatusService.All(room.Id).Where(x => x.Status.Contains("مشغولة")).SingleOrDefault().Id;
        //                }
        //                if (RoomStatus_ID!=0)
        //                    {
        //                        _roomService.EditRoomStat(room.Id, RoomStatus_ID);
        //                        foreach (var item1 in _userService.GetAll())
        //                        {
        //                            if (_userService.HasRole(item1.UserId, "ReservationEmp"))
        //                            {
        //                                NotifictationDto dto = new NotifictationDto();
        //                                dto.senderId = Guid.Parse(User.Identity.GetUserId());
        //                                dto.RevieverId = item1.UserId;
        //                                if (valueStat.Contains("تنظيف"))
        //                                {
        //                                    dto.NotText = "تغيرت حالة الغرفة واصبحت بحاجة تنظيف" + "&" + valueNum;
        //                                }
        //                                if (valueStat.Contains("صيانة"))
        //                                {
        //                                    dto.NotText = "تغيرت حالة الغرفة واصبحت بحاجة صيانة" + "&" + valueNum;
        //                                }
        //                                if (valueStat.Contains("فارغة"))
        //                                {
        //                                    dto.NotText = "تغيرت حالة الغرفة واصبحت  فارغة" + "&" + valueNum;
        //                                }
        //                                if (valueStat.Contains("مشغولة"))
        //                                {
        //                                    dto.NotText = "تغيرت حالة الغرفة واصبحت  مشغولة" + "&" + valueNum;
        //                                }
        //                                dto.NotDateTime = DateTimeHelper.ConvertDateToString(Utils.ServerNow.Date, DateFormats.DD_MM_YYYY);
        //                                _notifictationService.Add(dto);
        //                                dto = new NotifictationDto();
        //                            }
        //                        }
        //                    }

        //                }

        //            }
        //        }


        //        return RedirectToAction("importFromExcel");

        //    }
        //}
    } 
}