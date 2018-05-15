using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace _00003741_DBSD_CW2.DataAccess
{
    public class DbMaster
    {
        public static string ConnStr
        {
            get
            {
                return WebConfigurationManager
                    .ConnectionStrings["ConnStrDB"]
                    .ConnectionString;
            }
        }


        public static bool DatabaseExists()
        {
            try
            {
                var allCustomers = new DatabaseManager().GetAllProducts();
                return allCustomers.Count > 0;              
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void CreateDatabase()
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/marketDB.sql");

            string script = File.ReadAllText(path);

            SqlConnection conn = new SqlConnection(ConnStr);

            Server server = new Server(new ServerConnection(conn));

            server.ConnectionContext.ExecuteNonQuery(script);
        }
        public static void CreateDatabaseIfNotExists()
        {
            if (!DatabaseExists())
                CreateDatabase();
        }
    }
}