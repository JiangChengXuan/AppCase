using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string sql = "select * from Students";

            DBHelper db = DBFactory.CreateDBHelper();

            DataTable dt = db.ExecuteQuery(sql);

            string debug = string.Empty;
        }
    }
}
