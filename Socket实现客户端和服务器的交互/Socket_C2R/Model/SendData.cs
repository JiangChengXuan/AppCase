using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// 客户端和服务器发送数据类
    /// </summary>
  public abstract class SendData
    {

      public abstract string DataHandle(byte [] data,int readCount,DataType dataType);

    }
}
