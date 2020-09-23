using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using System.Threading;
using System.IO;
namespace Server
{
    public partial class ServerMain : Form
    {
        public ServerMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// key：IP
        /// value：客户端通信套接字
        /// </summary>
        Dictionary<string, Socket> ClientSocketInfo = new Dictionary<string, Socket>();

        private void ServerMain_Load(object sender, EventArgs e)
        {
            //取消跨线程访问
            Control.CheckForIllegalCrossThreadCalls = false;
            cboUsers.Items.Add("请选择要发送数据的客户端");
            cboUsers.SelectedIndex = 0;
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //创建负责监听的套接字
            Socket socketListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //创建网络信息对象
            IPAddress ip = IPAddress.Parse(txtServer.Text);
            IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));

            //监听套接字绑定网络信息，对该端口进行监听
            socketListen.Bind(point);

            //进行监听
            socketListen.Listen(10);
            txtLog.AppendLineText("监听成功");

            //获取连接后创建负责与客户端通信的套接字
            Thread th = new Thread(AcceptCilent);
            th.IsBackground = true;
            th.Start(socketListen);

        } // END 开始监听（）


        /// <summary>
        /// 接收客户端的连接并创建与之通信的套接字
        /// </summary>
        /// <param name="socket">负责监听的套接字</param>
        private void AcceptCilent(object socket)
        {
            Socket socketListen = socket as Socket;

            while (true)
            {
                #region 创建与客户端通信的套接字
                Socket socketClient = socketListen.Accept();
                string remoteInfo = socketClient.RemoteEndPoint.ToString(); //客户端的IP+端口
                txtLog.AppendLineText(remoteInfo + "：连接成功"); 
                #endregion

                //将往来连接的客户端套接字存储
                SaveClientSocketInfo(remoteInfo, socketClient);

                Thread th = new Thread(ReadClientData);
                th.IsBackground = true;
                th.Start(socketClient);

            }

        } // END AcceptCilent（）


        /// <summary>
        /// 读取客户端传输的数据
        /// </summary>
        private void ReadClientData(object socket)
        {
            Socket socketClient = socket as Socket;

            byte [] buffer=new byte[1024*1024];
            int readCount = 0;

            try
            {
                while ((readCount = socketClient.Receive(buffer)) > 0)
                {
                    string text = Encoding.UTF8.GetString(buffer, 0, readCount);
                    string remoteInfo = socketClient.RemoteEndPoint.ToString();
                    txtLog.AppendLineText(remoteInfo + "：" + text);
                }
            }
            catch (Exception)
            {
                 
            }

        }// END ReadClientData（）


        /// <summary>
        /// 将建立连接的客户端套接字存储
        /// </summary>
        /// <param name="remoteInfo">客户端IP信息</param>
        /// <param name="socketClient">客户端套接字</param>
        private void SaveClientSocketInfo(string remoteInfo, Socket socketClient)
        {
            if (ClientSocketInfo.ContainsKey(remoteInfo)) return;

            ClientSocketInfo.Add(remoteInfo, socketClient);
            cboUsers.Items.Add(remoteInfo);

        }// END SaveClientSocketInfo()


        /// <summary>
        /// 向客户端发送文本数据
        /// </summary>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMsg.Text))
            {
                MessageBox.Show("发送消息不能为空");
                return;
            }


            if (cboUsers.SelectedIndex==0)
            {
                 MessageBox.Show("请选择你要发送的客户端");
                return;
            }


            string clientInfo = cboUsers.SelectedItem.ToString(); //选择要发送的客户端的IP信息
            Socket socket = ClientSocketInfo[clientInfo]; //根据IP选取对应的客户端套接字
          
            //整理要发送的消息
            byte [] msg = Encoding.UTF8.GetBytes(txtMsg.Text);
            List<byte> sendMsg = msg.ToList();
            sendMsg.Insert(0, (int)DataType.text); //加入发送的数据类型

            socket.Send(sendMsg.ToArray());

        }// END 向客户端发送数据（）



        /// <summary>
        /// 选择文件
        /// </summary>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.InitialDirectory = @"C:\Users\Administrator\Desktop";
            dia.Filter = "文本文件|*.txt|图片文件|*.jpg";

            if (dia.ShowDialog()== DialogResult.OK)
            {
                txtPath.Text = dia.FileName;
            }

        } // END 选择文件（）



        /// <summary>
        /// 发送文件
        /// </summary>
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("请选择你要发送的文件");
                return;
            }

            if (cboUsers.SelectedIndex == 0)
            {
                MessageBox.Show("请选择你要发送的客户端");
                return;
            }

            //获取到于客户端通信的套接字
            string clientInfo = cboUsers.SelectedItem.ToString(); //选择要发送的客户端的IP信息
            Socket socket = ClientSocketInfo[clientInfo]; //根据IP选取对应的客户端套接字

            //读取文件流
            using (Stream fsRead=new FileStream(txtPath.Text,FileMode.Open,FileAccess.Read))
            {
                 byte [] buffer=new byte[fsRead.Length];
                 fsRead.Read(buffer, 0, buffer.Length);

                //获取文件类型
                 string strType = txtPath.Text.Substring(txtPath.Text.LastIndexOf(".") + 1).ToLower();
                DataType dataType= (DataType)Enum.Parse(typeof(DataType), strType);
                byte typeFlag = (byte)dataType;

                //在字节数组中加入文件类型标识
                List<byte> sendData = buffer.ToList();
                sendData.Insert(0, typeFlag);

                //发送
                socket.Send(sendData.ToArray());

            }  


        } // END 发送文件（）

    }
}
