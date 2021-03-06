﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomServer.HttpModel
{
    /// <summary>
    /// 用于异步读取客户端数据的对象
    /// </summary>
    public class HttpRequest
    {


      List<string> FileTypeList = new List<string>() { "\\.html", "\\.jpg" };
      List<string> RptType = new List<string>() { "get", "post" };
    public  Dictionary<string, string> ParameterDic = new Dictionary<string, string>();


      public string RequestFilePath { get; set; }

        /// <summary>
        /// 服务器目录
        /// </summary>
        public string ServerPath { get; set; }

        /// <summary>
        /// 请求的资源文件
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Http 请求方式
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// 请求的资源文件的扩展名
        /// </summary>
        public string FileExtName { get; set; }


        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        /// <summary>
        /// 客户端请求报文
        /// </summary>
        public StringBuilder RqtMsg = new StringBuilder();

        /// <summary>
        /// 加载客户端请求报文中的信息
        /// </summary>
        private void LoadRequestInfo()
        {
         
            Match match = null;
            string pattern = string.Empty;
            string RequestMsg = RqtMsg.ToString();

            if (Regex.IsMatch(RequestMsg,"favicon.ico"))
            {
                return;
            }

            //获取请求方式
            foreach (string type in RptType)
            {
                pattern = "^" + type;
                match = Regex.Match(RequestMsg, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    this.RequestType = match.Value;
                    break;
                }
            }


            //获取请求资源文件扩展名
            foreach (string type in  FileTypeList)
            {
                pattern = type;
                match = Regex.Match(RequestMsg, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    this.FileExtName = match.Value;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(FileExtName)) 
            {
                //获取文件名
                pattern = string.Format("(?<=/)\\w+(?={0})", FileExtName);
                match = Regex.Match(RequestMsg, pattern);

                if (match.Success)
                {
                    FileName = match.Value;
                }
            }

          

        } // END LoadRequestInfo（）


        public void WriteToMsg(string msg)
        {
            this.RqtMsg.Append(msg);
            LoadRequestInfo();
            GetParameter();
        }


        /// <summary>
        /// 处理请求资源
        /// </summary>
        /// <returns></returns>
        public byte[] ProcessRequest(out int httpState)
        {
            httpState = 200;
            List<byte> buffer = new List<byte>();
            byte[] temp = new byte[0];

            #region 处理文件

            //解析请求报文时，报文中没有请求指定文件 2020-7-9 Modify
            if (string.IsNullOrEmpty(FileName)&&string.IsNullOrEmpty(FileExtName))
            {
                buffer.AddRange(GetDefaultPage());
            }
            else
            {
                //读取请求的文件资源          
                string fullFilePath = ServerPath + "\\" + FileName + FileExtName;
                if (!File.Exists(fullFilePath)) //文件不存在
                {
                    httpState = 404;
                    string msg = "404 请求资源不存在";
                    temp = Encoding.UTF8.GetBytes(msg);
                    buffer.AddRange(temp);
                }
                else
                {
                    using (Stream fsRead = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read))
                    {
                        temp = new byte[fsRead.Length];
                        fsRead.Read(temp, 0, temp.Length);
                        buffer.AddRange(temp);
                        this.RequestFilePath = fullFilePath;
                    }
                }
      

            }

      
     
            #endregion
 

            //解析请求参数
            if (ParameterDic.Count>0)
            {
                 temp = ProcessHttp();
                buffer.AddRange(temp);
            }

            return buffer.ToArray();
        }  // END ReadRequestMsg（）


        /// <summary>
        /// 获取请求传递的参数
        /// </summary>
        private void GetParameter()
        {
            string[] dataLines = Regex.Split(this.RqtMsg.ToString(), "\\r\\n");
            if (dataLines == null || dataLines.Length <= 0) return;
           

            string firstRow = dataLines[0];
            Match match = Regex.Match(firstRow, "(?<=\\?).+\\s");
            if (!match.Success) return;
           

            string[] arr = match.Value.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in arr)
            {
                string key = item.Substring(0, item.IndexOf('='));
                string val = item.Substring(item.IndexOf('=') + 1);
                if (ParameterDic.ContainsKey(key)) continue;

                ParameterDic[key] = val;
            }

        } // END GetParameter（）

        public byte[] ProcessHttp()
        {
            byte[] buffer = new byte[0];
            string temp = string.Empty;

            foreach (KeyValuePair<string,string> para in ParameterDic)
            {
                temp += string.Format("参数名：{0}，参数值：{1}\r\n", para.Key, para.Value);
            }

            if (temp.Length>0)
            {
             buffer=   Encoding.UTF8.GetBytes(temp);
            }
            return buffer;
        } // END ProcessHttp（）


        /// <summary>
        /// 处理请求的文件资源
        /// </summary>
        /// <returns>文件字节数组</returns>
        private byte[] ProcessFile()
        {
            byte[] buffer = new byte[0];

            //读取资源
            string fullFilePath = ServerPath + "\\" + FileName + FileExtName;
            if (!File.Exists(fullFilePath))
            {
                string temp = "404 服务器没有";
                return buffer;
            }

            using (Stream fsRead = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fsRead.Length];
                fsRead.Read(buffer, 0, buffer.Length);
            }


            return buffer;
        }  // END ProcessFile（）


        /// <summary>
        /// 请求方没有指定请求的文件时，返回默认页面
        /// 2020-7-9 Modify
        /// </summary>
        /// <returns>默认文件的字节数组</returns>
        private byte[] GetDefaultPage()
        {


            string defaultFilePath = Path.Combine(this.ServerPath, "default.html");
            if (!File.Exists(defaultFilePath))
            {
                return new byte[0];
            }

            using (Stream fsRead=new FileStream(defaultFilePath,FileMode.Open,FileAccess.Read))
            {
                byte[] buffer = new byte[fsRead.Length];
              int count=  fsRead.Read(buffer, 0, buffer.Length);
              this.RequestFilePath = defaultFilePath;
              this.FileExtName = ".html";
              if (count==buffer.Length)
                {
                    return buffer;
                }
            }

            return new byte[0];  
        } // END GetDefaultPage（）


    } // END class
}
