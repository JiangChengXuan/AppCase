using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace Client
{
    public partial class ClientMain : Form
    {
        public ClientMain()
        {
            InitializeComponent();
        }

        Socket socketToServer;

        private void ClientMain_Load(object sender, EventArgs e)
        {
             //取消跨线程
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //创建与服务端通信的套接字
              socketToServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //创建网络信息对象
            IPAddress ip = IPAddress.Parse(txtServer.Text);
            IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));

            //创建连接
            socketToServer.Connect(point);
            txtLog.AppendLineText("连接成功");

            //接收服务端发送的数据
            Thread th = new Thread(ReadServerData);
            th.IsBackground = true;
            th.Start(socketToServer);

        } // END 建立连接（）


        /// <summary>
        /// 读取服务端发来的数据
        /// </summary>
        /// <param name="socket"></param>
        private void ReadServerData(object socket)
        {
            Socket socketToServer = socket as Socket;
            string serverInfo = socketToServer.RemoteEndPoint.ToString();

            byte[] buffer = new byte[1024 * 1024*5];
            int readCount = 0;

            try
            {
                while ((readCount = socketToServer.Receive(buffer)) > 0)
                {
                    //获取发送数据的类型
                    List<byte> data = buffer.ToList();
                    DataType dataType = (DataType)data[0]; //如果数字不在枚举范围内则会发送异常！！！！
                    data.RemoveAt(0); //将类型标识从数据字节中删除
                    readCount = readCount - 1;
 
                    //处理数据
                    SendData sendData = DataFactory.GetSendData(dataType);
                    string result = sendData.DataHandle(data.ToArray(), readCount, dataType);

                    //输出处理结果
                    txtLog.AppendLineText(result);

                }
            }
            catch (Exception)
            {
                
            }

        }  // END ReadServerData（）


        /// <summary>
        /// 向服务端发送数据
        /// </summary>
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte []  msg = Encoding.UTF8.GetBytes(txtMsg.Text);
            socketToServer.Send(msg);

        } // END 向服务端发送数据（）


    }
}
