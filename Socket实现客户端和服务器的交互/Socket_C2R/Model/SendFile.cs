using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Model
{
    public class SendFile : SendData
    {

        /// <summary>
        /// 将服务端发送给客户端的文件进行存储
        /// </summary>
        /// <param name="data">文件数据源</param>
        /// <param name="readCount">文件实际的字节数</param>
        /// <param name="dataType">文件类型</param>
        public override string DataHandle(byte[] data, int readCount, DataType dataType)
        {
            //文件存储全路径
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + Guid.NewGuid() + "." + dataType;

                using (Stream fsWrite = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fsWrite.Write(data, 0, readCount);
                }

            }
            catch (Exception)
            {
                return "文件存储失败";
                
            }
            return "文件已保存";
        }


    }

}
