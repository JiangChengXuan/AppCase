using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class DataFactory
    {

       public static SendData GetSendData(DataType dataType)
       {
           SendData sendData = null;

           switch (dataType)
           {
               case DataType.text:
                   sendData = new SendText();
                   break;
               case DataType.txt:
               case DataType.jpg:
                   sendData = new SendFile();
                   break;
               default:
                   break;
           }

           return sendData;
       } // END GetSendData（）

    }
}
