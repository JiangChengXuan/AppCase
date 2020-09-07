using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace DBAccess
{
    public class SQLHelper : DBHelper
    {

        public override int ExecuteNonQuery(string sql)
        {
            using (SqlConnection con = new SqlConnection(base.ConStr))
            using (SqlCommand com = new SqlCommand(sql, con))
            {
                con.Open();
                return com.ExecuteNonQuery();
            }


        } // END ExecuteNonQuery（）

        public override DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(base.ConStr))
            using (SqlCommand com = new SqlCommand(sql, con))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
            }

            return dt;
        }  // END ExecuteQuery（）

        public override object ExecuteScalar(string sql)
        {
            using (SqlConnection con = new SqlConnection(base.ConStr))
            using (SqlCommand com = new SqlCommand(sql, con))
            {
                con.Open();
                return com.ExecuteScalar();
            }
        }  // END ExecuteScalar（）

    }
}
