using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApplication
{
    /// <summary>
    /// UploadImg 的摘要说明
    /// </summary>
    public class UploadImg : IHttpHandler
    {
       

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            #region Step1-图片上传
          
            ImgUpload upload = new ImgUpload(context.Request.Files[0]);
            string errMsg = string.Empty;
            if (!upload.Upload(out errMsg)) //上传失败
            {
                context.Response.Write(errMsg);
                return;
            } 
            #endregion

            #region Step2-新增头像信息数据
  
            //获取客户端表单信息
            string name = context.Request.Form["txtName"];
            string path = upload.SavePath;

            //封装数据进行录入
            AvatarDAL xml = new AvatarDAL(context.Server.MapPath("/SimpleDB.xml"));
            AvatarInfo data = new AvatarInfo();
            data.ID = Guid.NewGuid().ToString(); ;
            data.Name = name;
            data.Path = path;
            data.HttpContentType = upload.HttpContentType;
            xml.CreateAvatarNode(data); 

            #endregion


            //Step3-转到列表页面
            context.Response.Redirect("/ShowAvatar.ashx");
             

        }  // END ProcessRequest（）

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}