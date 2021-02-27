using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NawafizApp.Web.Helper
{
    public static class MysqlFetchingRoomData
    {

        public static List<MySQlRoom> getDataFromMySql()
        {
            List<MySQlRoom> mySQlRooms = new List<MySQlRoom>();
            string connStr = "server=localhost;database=account_1;port=3306;SslMode=none;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                List<DataRow> list = new List<DataRow>();
                try
                {
                    conn.Open();

                    string sql = "select room_code,room_ClnStatus,room_MenStatus from Hot_Room;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(rdr);
                    IEnumerable<DataRow> collection = dt.Rows.Cast<DataRow>();
                    mySQlRooms = new List<MySQlRoom>();
                    foreach (var item in collection)
                    {
                        mySQlRooms.Add(new MySQlRoom()
                        {
                            CleanStatus = item.ItemArray[1].ToString(),
                            MantStatus = item.ItemArray[2].ToString(),
                            RoomNum = item.ItemArray[0].ToString()
                        });
                    }

                    //list = dt.AsEnumerable().ToList();
                    conn.Close();

                }
                catch (Exception e)
                {
                    return new List<MySQlRoom>();

                }

            }
            return mySQlRooms;


        }
    }

    public class MySQlRoom
    {
        public string RoomNum { get; set; }
        public string CleanStatus { get; set; }
        public string MantStatus { get; set; }

    }
}