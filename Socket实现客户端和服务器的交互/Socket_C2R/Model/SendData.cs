using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public abstract class SendData
    {

      public abstract string DataHandle(byte [] data,int readCount,DataType dataType);

    }
}
