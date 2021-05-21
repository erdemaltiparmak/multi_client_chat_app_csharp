using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class Logger
    {
        public static void Log(string mesaj,int gonderenId,int aliciId)
        {
            var connection= Database.Connection();
            connection.Open();

            SqlCommand query = new SqlCommand("",connection);


        }
    }
}
