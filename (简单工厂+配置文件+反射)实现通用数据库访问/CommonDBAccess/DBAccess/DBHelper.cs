using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
namespace DBAccess
{
    public abstract class  DBHelper
    {

        public DBHelper()
        {
            LoadConStr();
        }

        #region 连接字符串

        private string _ConStr;
        public string ConStr
        {
            get { return _ConStr; }
            private set { _ConStr = value; }
        } 

        #endregion


        //public DbConnection  DBCon { get; set; }

        /// <summary>
        /// 读取数据库连接字符串
        /// </summary>
        private void LoadConStr()
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString ?? "";
            this.ConStr = conStr;



        } // END LoadConStr（）

        /// <summary>
        /// 增删改
        /// </summary>
        public abstract int ExecuteNonQuery(string sql);
       


        /// <summary>
        /// 查询
        /// </summary>
        public abstract DataTable ExecuteQuery(string sql);


        /// <summary>
        /// 执行查询，返回单行单列数据
        /// </summary>
        public abstract object ExecuteScalar(string sql);


    }
}
