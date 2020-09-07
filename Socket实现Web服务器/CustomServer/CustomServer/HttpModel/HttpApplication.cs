using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace CustomServer.HttpModel
{
    /// <summary>
    /// 处理客户端发送的数据
    /// </summary>
  public  class HttpApplication
    {
  
      public Socket SocketHandle { get; set; }

      public HttpApplication(Socket socket)
      {
          this.SocketHandle = socket;
      }


      /// <summary>
      /// 获取客户端发送过来的数据
      /// </summary>
      public string GetRequestMsg()
      {
          string clientInfo = string.Empty;
          byte [] buffer=new  byte[1024*1024*2];
         int readCount= SocketHandle.Receive(buffer);

         if (readCount>=0)
         {
                   clientInfo=  Encoding.UTF8.GetString(buffer);
         }

         return clientInfo;
      }  // END GetClientInfo（）



    }
}
