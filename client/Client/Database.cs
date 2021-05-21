using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Client
{
    public static class Database
    {
        static string BaglantiAdresi = @"Server=DESKTOP-6201QPP\SQLEXPRESS;Database=AgProgDB;Trusted_Connection=True;";


        public static SqlConnection Connection()
        {
            SqlConnection connection = new SqlConnection(BaglantiAdresi);
            return connection;
        }
    }
}
