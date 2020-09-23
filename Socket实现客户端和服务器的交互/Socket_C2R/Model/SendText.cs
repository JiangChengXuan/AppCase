using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class SendText:SendData
    {


      public override string DataHandle(byte[] data, int readCount, DataType dataType)
      {
           return Encoding.UTF8.GetString(data.ToArray(), 0, readCount);
  
      }

    }
}
