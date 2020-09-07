using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace WebApplication
{
    /// <summary>
    /// ShowAvatar 的摘要说明
    /// </summary>
    public class ShowAvatar : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            //获取数据源
            AvatarDAL dal = new AvatarDAL(context.Server.MapPath("/SimpleDB.xml"));
            List<AvatarInfo> list = dal.GetAvatarInfo();

            //读取HTML模板
            string htmlTemp = File.ReadAllText(context.Server.MapPath("/AvatarView.html"));

            //将数据填充到模板
            string content = string.Empty;
            foreach (AvatarInfo item in list)
            {
                string src = item.Path.Replace(context.Server.MapPath("/"), "");
                string img = string.Format("<img class='AvatarImg' src={0}  />", src);
                content += string.Format("<tr><td>{0}</td><td>{1}</td><td><a  href='/DownLoad.ashx?id={2}' >下载</a></td></tr>"
                    , item.Name, img, item.ID);
            }
            htmlTemp = htmlTemp.Replace("@data",content);

            context.Response.Write(htmlTemp);
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