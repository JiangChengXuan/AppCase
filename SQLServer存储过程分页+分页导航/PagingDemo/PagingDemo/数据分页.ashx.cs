using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
namespace PagingDemo
{
    /// <summary>
    /// 分页数据类
    /// </summary>
    public class PageData
    {

        /// <summary>
        /// 需要分页的表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 排序的字段
        /// </summary>
        public string GroupByField { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }


        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }

    }

    /// <summary>
    /// 数据分页 的摘要说明
    /// </summary>
    public class 数据分页 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            #region Step1-组织分页条件

            string index = context.Request.QueryString["index"]; //客户端传的页码
            PageData pageData = new PageData();
            pageData.TableName = "Students";
            pageData.GroupByField = "Studentid";
            pageData.PageSize = 5;

            //第一次加载时未传页码，所以默认为第一页
            pageData.PageIndex = string.IsNullOrEmpty(index) ? 1 : Convert.ToInt32(index);

            #endregion


            #region Step2-获取数据展示模板

            string htmlTemp = File.ReadAllText(context.Server.MapPath("数据模板.html"));
            string htmlStr = string.Empty;

            #endregion

            //Step3-获取当前页数据
            DataTable dt = LoadData(pageData);

            #region Step4-绘制表格

            string tTitle = string.Empty; //标题
            int colsCount = 1;
            foreach (DataRow row in dt.Rows)
            {
                htmlStr += "<tr>";

                foreach (DataColumn col in dt.Columns)
                {
                    if (colsCount <= dt.Columns.Count)  //生成标题
                    {
                        tTitle += string.Format("<td>{0}</td>", col.ColumnName);
                        colsCount++;
                    }

                    //生成单元格数据
                    htmlStr += string.Format("<td>{0}</td>", row[col.ColumnName]);

                }
                htmlStr += "</tr>";
            }

            htmlStr = htmlStr.Insert(0, tTitle); // 追加表格标题
            htmlTemp = htmlTemp.Replace("@data", htmlStr); //填充表格

            #endregion



            #region Step5-绘制分页导航

            //#region 上一页按钮
            //if (pageData.PageIndex > 1) //不是第一页的情况
            //{
            //    int num = pageData.PageIndex - 1;
            //    string prev = string.Format("href='/数据分页.ashx?index={0}'", num);
            //    var n = pageData.PageIndex - 1;
            //    htmlTemp = htmlTemp.Replace("@s", prev);
            //}
            //#endregion

            //#region 下一页按钮
            //if (pageData.PageIndex < pageData.PageCount) //不是最后一页
            //{
            //    int num = pageData.PageIndex + 1;
            //    string next = string.Format("href='/数据分页.ashx?index={0}'", num);
            //    htmlTemp = htmlTemp.Replace("@x", next);
            //}
            //#endregion

           string navStr= PagerHelper.strPage(pageData, "/数据分页.ashx?index=");
           htmlTemp = htmlTemp.Replace("@navPage", navStr);
            #endregion


            #region Step6-响应客户端
            context.Response.ContentType = "text/html";
            context.Response.Write(htmlTemp);
            #endregion

        }

        /// <summary>
        /// 根据分页条件获取指定页的数据
        /// </summary>
        /// <param name="pageData">分页数据对象</param>
        /// <returns>指定页的数据</returns>
        private DataTable LoadData(PageData pageData)
        {
            using (SqlConnection con = new SqlConnection("Data Source=192.168.31.11;Initial Catalog=StudentManager;User ID=sa;Password=123"))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_DataPaging", con);
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("pageIndex",pageData.PageIndex),
                   new SqlParameter("pageSize",pageData.PageSize),
                   new SqlParameter("tableName",pageData.TableName),
                    new SqlParameter("orderByField",pageData.GroupByField),
                        new SqlParameter("rowCount",SqlDbType.Int),
                         new SqlParameter("pageCount",SqlDbType.Int)
                };
                com.Parameters.AddRange(paras);

                paras[4].Direction = ParameterDirection.Output;
                paras[5].Direction = ParameterDirection.Output;

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);

                pageData.RowCount = Convert.ToInt32(paras[4].Value);
                pageData.PageCount = Convert.ToInt32(paras[5].Value);

                return dt;
            }

        } // END LoadData（）


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}