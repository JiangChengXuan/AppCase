using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
namespace DBAccess
{
    public class OracleHelper : DBHelper
    {
        public override int ExecuteNonQuery(string sql)
        {
            using (OracleConnection con = new OracleConnection(base.ConStr))
            using (OracleCommand com = new OracleCommand(sql, con))
            {
                con.Open();
                return com.ExecuteNonQuery();
            }


        } // END ExecuteNonQuery（）

        public override DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();

            using (OracleConnection con = new OracleConnection(base.ConStr))
            using (OracleCommand com = new OracleCommand(sql, con))
            {
                con.Open();
                OracleDataAdapter da = new OracleDataAdapter(com);
                da.Fill(dt);
            }

            return dt;
        }  // END ExecuteQuery（）

        public override object ExecuteScalar(string sql)
        {
            using (OracleConnection con = new OracleConnection(base.ConStr))
            using (OracleCommand com = new OracleCommand(sql, con))
            {
                con.Open();
                return com.ExecuteScalar();
            }
        }  // END ExecuteScalar（）




    }
}
