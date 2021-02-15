using NawafizApp.Services;
using NawafizApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NawafizApp.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //private static void _SetupRefreshJob()
        //{

        //    //remove a previous job
        //    Action remove = HttpContext.Current.Cache["Refresh"] as Action;
        //    if (remove is Action)
        //    {
        //        HttpContext.Current.Cache.Remove("Refresh");
        //        remove.EndInvoke(null);
        //    }

        //    //get the worker
        //    Action work = () => {
        //        while (true)
        //        {
        //            Thread.Sleep(60000);
        //            WebClient refresh = new WebClient();
        //            try
        //            {
        //                refresh.UploadString("http://salesapp.ssg-sy.com", string.Empty);
        //                refresh.UploadString("http://salesapp.ssg-sy.com/remote", string.Empty);
        //            }
        //            catch (Exception ex)
        //            {
        //                //snip...
        //            }
        //            finally
        //            {
        //                refresh.Dispose();
        //            }
        //        }
        //    };
        //    work.BeginInvoke(null, null);

        //    //add this job to the cache
        //    HttpContext.Current.Cache.Add(
        //        "Refresh",
        //        work,
        //        null,
        //        Cache.NoAbsoluteExpiration,
        //        Cache.NoSlidingExpiration,
        //        CacheItemPriority.Normal,
        //        (s, o, r) => { _SetupRefreshJob(); }
        //        );
        //}
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Initialize Mapper
            DtoMappings.Initialize();

            //Initialise Bootstrapper
            Bootstrapper.Initialise();


          

        }
    }
}
