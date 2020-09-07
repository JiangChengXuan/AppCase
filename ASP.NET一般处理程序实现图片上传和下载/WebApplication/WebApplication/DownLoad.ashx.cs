using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
namespace WebApplication
{
    /// <summary>
    /// DownLoad 的摘要说明
    /// </summary>
    public class DownLoad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            #region Step1-通过ID获取头像信息数据对象
        
            string ID = context.Request.QueryString["id"] == null ? "" : context.Request.QueryString["id"];
            AvatarDAL dal = new AvatarDAL(context.Server.MapPath("/SimpleDB.xml"));
            AvatarInfo data = dal.GetAvatarInfoByID(ID); 
            #endregion

            #region Step-2获取文件流
      
            string fileName = Path.GetFileName(data.Path); //从决定路径中获取文件名
            fileName=HttpUtility.UrlEncode(fileName, Encoding.UTF8); //防止中文文件名出现乱码

            FileStream fsRead = new FileStream(data.Path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fsRead.Length];
            fsRead.Read(buffer, 0, buffer.Length);
            fsRead.Close();

            #endregion


            #region Step3-反馈下载信息给客户端
 
            //在向客户端响应之前，设置报文头，告诉客户端返回的内容是一个附件，需要下载
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename='{0}'", fileName));

            //以流的形式输出
            context.Response.BinaryWrite(buffer);
            context.Response.Flush();
            context.Response.End();
            #endregion

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}