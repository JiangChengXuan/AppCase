using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


using System.Data.SQLite;

namespace DBAccess
{
    public class SQLiteHelper : DBHelper
    {

        public override int ExecuteNonQuery(string sql)
        {
            using (SQLiteConnection con = new SQLiteConnection(base.ConStr))
            using (SQLiteCommand com = new SQLiteCommand(sql, con))
            {
                con.Open();
                return com.ExecuteNonQuery();
            }


        } // END ExecuteNonQuery（）

        public override DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();

            using (SQLiteConnection con = new SQLiteConnection(base.ConStr))
            using (SQLiteCommand com = new SQLiteCommand(sql, con))
            {
                con.Open();
                SQLiteDataAdapter da = new SQLiteDataAdapter(com);
                da.Fill(dt);
            }

            return dt;
        }  // END ExecuteQuery（）

        public override object ExecuteScalar(string sql)
        {
            using (SQLiteConnection con = new SQLiteConnection(base.ConStr))
            using (SQLiteCommand com = new SQLiteCommand(sql, con))
            {
                con.Open();
                return com.ExecuteScalar();
            }
        }  // END ExecuteScalar（）





    }
}
