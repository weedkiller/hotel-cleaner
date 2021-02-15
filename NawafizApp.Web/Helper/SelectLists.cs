using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawafizApp.Web.Helper
{
    public static class SelectLists
    {


    
        public static IList<SelectListItem> countinu()
        {
            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = true,
                                       Text = String.Empty,
                                       Value=""
                                   },
                               new SelectListItem
                                   {

                                       Text ="مستمر",
                                       Value=true.ToString()
                                   },
                               new SelectListItem
                                   {

                                       Text ="غير مستمر",
                                       Value=false.ToString()
                                   },


                           };
            return list;
        }


        public static IList<SelectListItem> getAllblock(int? selected)
        {
            var service = DependencyResolver.Current.GetService<Services.Interfaces.IHotelBlockService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                   
                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.GetAll().ToList()

                              .Select(x => new SelectListItem
                              {
                                  Selected = x.Id == (selected.HasValue ? selected.Value : -1),
                                  Text = x.BlockName,
                                  Value = x.Id.ToString()
                              })
                              .ToList());

            return list;
        }

        public static IList<SelectListItem> getAllstate(int? selected)
        {
            var service = DependencyResolver.Current.GetService<Services.Interfaces.IRoomStatusService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {

                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.getall().ToList()

                              .Select(x => new SelectListItem
                              {
                                  Selected = x.Id == (selected.HasValue ? selected.Value : -1),
                                  Text = x.Status,
                                  Value = x.Id.ToString()
                              })
                              .ToList());

            return list;
        }


        public static IList<SelectListItem> getromeType(int? selected)
        {
            var service = DependencyResolver.Current.GetService<Services.Interfaces.IRoomTypeService>();

            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {

                                       Selected = !selected.HasValue,
                                       Text = String.Empty,
                                       Value=""
                                   }
                           };

            list.AddRange(service.GetRoomTypes().ToList()

                              .Select(x => new SelectListItem
                              {
                                  Selected = x.Id == (selected.HasValue ? selected.Value : -1),
                                  Text = x.Name,
                                  Value = x.Id.ToString()
                              })
                              .ToList());

            return list;
        }


    }
}

