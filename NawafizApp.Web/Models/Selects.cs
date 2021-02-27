using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawafizApp.Web.Models
{
    public static class Selects
    {

        //[Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]


        public static IList<SelectListItem> CleanerUsers(int? selected)
        {
            var service = DependencyResolver.Current.GetService<IUserService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.GetAll().Where(x =>  service.HasRole(x.UserId, "Cleaner"))
                              .ToList()

                              .Select(x => new SelectListItem
                              {
                               
                                  Text = x.UserName,
                                  Value = x.UserId.ToString()
                              })
                              .ToList());

            return list;
        }
        public static IList<SelectListItem> MaintenanceEmpUsers(int? selected)
        {
            var service = DependencyResolver.Current.GetService<IUserService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.GetAll().Where(x =>  service.HasRole(x.UserId, "MaintenanceEmp"))
                              .ToList()

                              .Select(x => new SelectListItem
                              {

                                  Text = x.UserName,
                                  Value = x.UserId.ToString()
                              })
                              .ToList());

            return list;
        }
        public static IList<SelectListItem> ReservationEmpUsers(int? selected)
        {
            var service = DependencyResolver.Current.GetService<IUserService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.GetAll().Where(x =>  service.HasRole(x.UserId, "Hoster"))
                              .ToList()

                              .Select(x => new SelectListItem
                              {

                                  Text = x.UserName,
                                  Value = x.UserId.ToString()
                              })
                              .ToList());

            return list;
        }
        //hoster
        public static IList<SelectListItem>ServiceEmpUsers(int? selected)
        {
            var service = DependencyResolver.Current.GetService<IUserService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.GetAll().Where(x => service.HasRole(x.UserId, "service"))
                              .ToList()

                              .Select(x => new SelectListItem
                              {

                                  Text = x.UserName,
                                  Value = x.UserId.ToString()
                              })
                              .ToList());

            return list;
        }
        //BlockSupervisor
        public static IList<SelectListItem> ManagerEmpUsers(int? selected)
        {
            var service = DependencyResolver.Current.GetService<IUserService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.GetAll().Where(x =>  service.HasRole(x.UserId, "BlockSupervisor"))
                              .ToList()

                              .Select(x => new SelectListItem
                              {

                                  Text = x.UserName,
                                  Value = x.UserId.ToString()
                              })
                              .ToList());

            return list;
        }
        public static IList<SelectListItem> UsersEmp(int? selected)
        {
            var service = DependencyResolver.Current.GetService<IUserService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.GetAll().Where(x => service.HasRole(x.UserId, "MaintenanceEmp") || service.HasRole(x.UserId, "CleanEmp"))
                              .ToList()

                              .Select(x => new SelectListItem
                              {

                                  Text = x.UserName,
                                  Value = x.UserId.ToString()
                              })
                              .ToList());

            return list;
        }
        public static IList<SelectListItem> Rooms(int? selected)
        {
            var service = DependencyResolver.Current.GetService<IRoomService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.GetAll()
                              .ToList()

                              .Select(x => new SelectListItem
                              {

                                  Text = x.RoomNum,
                                  Value = x.Id.ToString()
                              })
                              .ToList());

            return list;
        }

        public static IList<SelectListItem> AveEmp(int? roomId)
        {
            var service = DependencyResolver.Current.GetService<IUserService>();
            var roomService = DependencyResolver.Current.GetService<IRoomService>();

            var currentRoom = roomService.GetById((int)roomId);
            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = false,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };
            var users = service.GetAll().Where(x => x.Hotelblock_id == currentRoom.HotelBlock_id).ToList();
            list.AddRange(users.Where(x => service.HasRole(x.UserId, "Cleaner"))
                              .ToList()

                              .Select(x => new SelectListItem
                              {

                                  Text = x.UserName,
                                  Value = x.UserId.ToString()
                              })
                              .ToList());

            return list;
        }

        public static IList<SelectListItem> AveMEmp(int? roomId)
        {
            var service = DependencyResolver.Current.GetService<IUserService>();
            var roomService = DependencyResolver.Current.GetService<IRoomService>();

            var currentRoom = roomService.GetById((int)roomId);

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = false,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };
            var users = service.GetAll().Where(x => x.Hotelblock_id == currentRoom.HotelBlock_id).ToList();
            list.AddRange(users.Where(x => service.HasRole(x.UserId, "MaintenanceEmp"))
                              .ToList()

                              .Select(x => new SelectListItem
                              {

                                  Text = x.UserName,
                                  Value = x.UserId.ToString()
                              })
                              .ToList());

            return list;
        }
    }

}