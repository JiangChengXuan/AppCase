using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomServer.HttpModel
{
   public class HttpResponse
    {
       Dictionary<string, string> ContentTypeInfo = new Dictionary<string, string>() { 
            {".html","text/html"},
                {".jpg","image/jpeg"}
                 
       };

        public byte []  AssetsByte { get; set; }
        public string ContentType { get; set; }
       public HttpResponse(byte []  bt,string fileExtName)
       {
           AssetsByte = bt;

           if (fileExtName !=null&& ContentTypeInfo.ContainsKey(fileExtName.ToLower()))
           {
               ContentType = ContentTypeInfo[fileExtName];
           }
        }


        /// <summary>
       /// 构建响应报文头
        /// </summary>
        /// <returns></returns>
       public byte[] GetHeaderResponse(int code)
       {
           StringBuilder builder = new StringBuilder();
           builder.AppendFormat("HTTP/1.1 {0} ok\r\n", code);
           builder.Append("Content-Type:" + ContentType + ";charset=utf-8\r\n");
           builder.Append("Content-Length:" + (AssetsByte.Length>0?AssetsByte.Length:0) + "\r\n\r\n"); 
           return System.Text.Encoding.UTF8.GetBytes(builder.ToString());

       } // END GetHeaderResponse（）
    

    }
}
