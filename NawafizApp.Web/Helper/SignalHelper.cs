using Microsoft.AspNet.SignalR;
using NawafizApp.Domain.Entities;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NawafizApp.Web.Helper
{
    public class SignalHelper
    {
        IUserService _userService;
        IFixOrderServices _fixOrderServices;
        /// <summary>
        /// بدء تشغيل مؤقت قراءة قيمة البطاقة
        /// </summary>
        /// <param name="user"></param>
        /// 
        public SignalHelper(ApplicationUserManager userManager, ApplicationSignInManager aps, IUserService IUS, IFixOrderServices fixOrderServices)
        {
           
            this._userService = IUS;
            
            this._fixOrderServices = fixOrderServices;
        }
        public static void Start()
        {
            var aTimer = new System.Timers.Timer();

            aTimer.Elapsed += (sender, e) => aTimer_Elapsed(sender, e);
            aTimer.Interval = 20000;
            aTimer.Enabled = true;
        }

        /// <summary>
        /// دالة تنفذ عند وصول مؤقت قراءة قيمة البطاقة إلى وقت القراءة
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        /// <param name="user">-</param>
        private static void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            Send(context);
            SendRoomsStatusChangedCount(context);
        }
        public static void SendRoomsStatusChangedCount(IHubContext context)
        {
            try
            {
                context.Clients.All.sendRoomNotification(GetCountOfNotOrderedRooms());
            }
            catch (Exception ex)
            {
            }
        }

        private static int GetCountOfNotOrderedRooms()
        {
            using (var l_oConnection = new SqlConnection(System.Configuration.ConfigurationManager.
                                                    ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    l_oConnection.Open();
                    // Create a String to hold the query.
                    string query = "select id from Rooms " +
                        "where (isneedclean = 1 and Isrequisted = 0) Or (Isrequistedfix=0 and IsNeedfix=1)";
                    ///
                    /// 
                    ///
                    /// 
                    //for Fixing manager
                    SqlCommand queryCommand = new SqlCommand(query, l_oConnection);
                    SqlDataReader queryCommandReaderFM = queryCommand.ExecuteReader();
                    DataTable dataTableFM = new DataTable();
                    dataTableFM.Load(queryCommandReaderFM);
                    IEnumerable<DataRow> collectionFM = dataTableFM.Rows.Cast<DataRow>();

                    var rowsCount = dataTableFM.Rows.Count;
                    l_oConnection.Close();
                    return rowsCount;
                }
                catch (SqlException ex)
                {
                    return 0;
                }
            }
        }

        public static void SendRoomsStatusChangedCount(int count)
        {
            try
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                context.Clients.All.sendRRoomNotification(count);
            }
            catch (Exception ex)
            {
            }
        }
        static void Send(IHubContext context)
        {
            try
            {
                 context.Clients.All.sendNotification(GetAllOrders());
            }
            catch (Exception ex)
            {
            }
        }
        public static List<OrdersInfo> GetAllOrders()
        {
            try
            {
                var orders = GetOrdersCount();

                return orders ;
            }
            catch(Exception ex)
            {
                return new List<OrdersInfo>();
            }
        }
        
        private static List<OrdersInfo> GetOrdersCount()
        {
            var result = new List<OrdersInfo>();
            using (var l_oConnection = new SqlConnection(System.Configuration.ConfigurationManager.
                                                   ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    l_oConnection.Open();
                    // Create a String to hold the query.
                    string queryCleanOrdersForCleaner = "select cleaningEmp from CleanOrders " +
                        "where isTaked = 0 and IsSeenFromCleaner = 0 and cleaningEmp is not null";
                    string queryCleanOrdersForManager = "select moshId from CleanOrders " +
                        "where isTaked = 0 and IsSeenFromManager = 0 and cleaningEmp is null";

                    string queryCleanOrdersForFixer = "select maitremp from FixOrders " +
                        "where isTaked = 0 and IsSeenFromFixer = 0 and maitremp is not null";
                    string queryCleanOrdersForFixerManager = "select moshId from FixOrders " +
                        "where isTaked = 0 and IsSeenFromManager = 0 and maitremp is null";

                    ///
                    /// 
                    ///
                    /// 
                    //for Fixing manager
                    SqlCommand queryCommand = new SqlCommand(queryCleanOrdersForFixerManager, l_oConnection);
                    SqlDataReader queryCommandReaderFM = queryCommand.ExecuteReader();
                    DataTable dataTableFM = new DataTable();
                    dataTableFM.Load(queryCommandReaderFM);
                    IEnumerable<DataRow> collectionFM = dataTableFM.Rows.Cast<DataRow>();
                    var ids = new List<string>();
                    foreach (var item in collectionFM)
                    {
                        ids.Add(item.ItemArray.First().ToString());
                    }
                    var rowsCount = dataTableFM.Rows.Count;
                    result.Add(new OrdersInfo()
                    {
                        Role = "MFixer",
                        UserIds = ids,
                        count = rowsCount
                    });
                    ///
                    // Create a SqlCommand object and pass the constructor the connection string and the query string.
                    SqlCommand queryCommand1 = new SqlCommand(queryCleanOrdersForCleaner, l_oConnection);

                    // Use the above SqlCommand object to create a SqlDataReader object.
                    SqlDataReader queryCommandReader = queryCommand1.ExecuteReader();
                    DataTable dataTable = new DataTable();

                    // Use the DataTable.Load(SqlDataReader) function to put the results of the query into a DataTable.
                    dataTable.Load(queryCommandReader);
                    IEnumerable<DataRow> collection = dataTable.Rows.Cast<DataRow>();
                    var ids1 = new List<string>();
                    foreach (var item in collection)
                    {
                        ids1.Add(item.ItemArray.First().ToString());
                    }
                    var rowsCount1 = dataTable.Rows.Count;
                    result.Add(new OrdersInfo()
                    {
                        Role = "ECleaner",
                        UserIds = ids1,
                        count = rowsCount1
                    });
                    //forCleaning and manager
                    queryCommand = new SqlCommand(queryCleanOrdersForManager, l_oConnection);

                    SqlDataReader queryCommandReaderCM = queryCommand.ExecuteReader();
                    DataTable dataTableCM = new DataTable();
                    dataTableCM.Load(queryCommandReaderCM);
                    IEnumerable<DataRow>  collectionCM = dataTableCM.Rows.Cast<DataRow>();
                    ids = new List<string>();
                    foreach (var item in collectionCM)
                    {
                        ids.Add(item.ItemArray.First().ToString());
                    }
                    rowsCount = dataTableCM.Rows.Count;
                    result.Add(new OrdersInfo()
                    {
                        Role = "MCleaner",
                        UserIds = ids,
                        count = rowsCount
                    });
                    //for Fixing 
                    queryCommand = new SqlCommand(queryCleanOrdersForFixer, l_oConnection);
                    SqlDataReader queryCommandReaderFE = queryCommand.ExecuteReader();
                    DataTable dataTableFE = new DataTable();
                    dataTableFE.Load(queryCommandReaderFE);
                    IEnumerable<DataRow> collectionFE = dataTableFE.Rows.Cast<DataRow>();
                    ids = new List<string>();
                    foreach (var item in collectionFE)
                    {
                        ids.Add(item.ItemArray.First().ToString());
                    }
                    rowsCount = dataTableFE.Rows.Count;
                    result.Add(new OrdersInfo()
                    {
                        Role = "EFixer",
                        UserIds = ids,
                        count = rowsCount
                    });
                    
                    //IEnumerable<DataRow> collection = returnData.Cast<DataRow>();
                    //var rowsCount = dataTable.Rows.Count;
                    l_oConnection.Close();
                    return result;
                }
                catch (SqlException ex)
                {
                    return result;
                }
            }
        }
        public class OrdersInfo
        {
            public OrdersInfo()
            {
                this.UserIds = new List<string>();
            }
              public string Role { get; set; }
              public int count { get; set; }
              public IList<string> UserIds { get; set; }
        }
    }
}