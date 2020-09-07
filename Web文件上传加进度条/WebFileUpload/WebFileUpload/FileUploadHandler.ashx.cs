using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
namespace WebFileUpload
{
    public class UploadInfo
    {
        public static long Goal { get; set; } //总长
        public static long Raised { get; set; } //当前完成率
    }

    /// <summary>
    /// FileUploadHandler 的摘要说明
    /// </summary>
    public class FileUploadHandler : IHttpHandler
    {
 

        public static Stream fsRead = null;
        public static Stream fsWrite = null;
        public static List<byte> list = new List<byte>();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            //生成文件存储路径
            string filePath = context.Server.MapPath("Files");
            string fileName = context.Request.Form["fileName"];
            filePath = Path.Combine(filePath, fileName);


            var file = context.Request.Files[0];
            filePath = filePath + Path.GetExtension(file.FileName);

            if (fsRead == null && fsWrite == null)  // 第一次
            {

                fsRead = file.InputStream;
                fsWrite = new FileStream(filePath, FileMode.Create, FileAccess.Write);


                byte[] buffer = new byte[1024 * 1024]; //每次读取1mb并写1mb
                int readByteCount = fsRead.Read(buffer, 0, buffer.Length);

                byte[] save = buffer.Skip(0).Take(readByteCount).ToArray();
                list.AddRange(save);

                UploadInfo.Goal = fsRead.Length;
                UploadInfo.Raised = readByteCount;
            }
            else  // 进行中
            {
                   file.InputStream.Position=fsRead.Position;
                fsRead=  file.InputStream;
                byte[] buffer = new byte[1024 * 1024]; //每次读取1mb并写1mb
                int readByteCount = fsRead.Read(buffer, 0, buffer.Length);

                byte[] save = buffer.Skip(0).Take(readByteCount).ToArray();
                list.AddRange(save);

                UploadInfo.Goal = fsRead.Length;
                UploadInfo.Raised = readByteCount;
            }



            JavaScriptSerializer js = new JavaScriptSerializer();
            var json = js.Serialize(new { Goal = UploadInfo.Goal, Raised = UploadInfo.Raised });
            context.Response.Write(json);
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