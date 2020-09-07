using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace GDI_生成验证码
{
    /// <summary>
    /// ValidateCodeHandler 的摘要说明
    /// </summary>
    public class ValidateCodeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpg";

            //生成验证码图片对象
            ValidateCode code = new ValidateCode();
            Image image = code.DrawCodeImg();

            //image.Save(context.Response.OutputStream, ImageFormat.Jpeg);

            //将图片对象加载到内存流
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);

            //以流的形式输出
            context.Response.Clear();
            context.Response.BinaryWrite(stream.ToArray());

            image.Dispose();
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