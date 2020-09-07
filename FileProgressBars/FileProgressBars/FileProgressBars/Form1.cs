using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace FileProgressBars
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "请选择要复制的文件";
            op.InitialDirectory = @"C:\Users\Administrator\Desktop";
            op.Filter = "所有文件|*.*";

            if (op.ShowDialog()==DialogResult.OK)
            {
                txtSource.Text = op.FileName;
            }

        }

        /// <summary>
        /// 保存文件
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "请选择要保存的文件路径";
            sd.InitialDirectory = @"C:\Users\Administrator\Desktop";
            sd.Filter = "所有文件|*.*";

            if (sd.ShowDialog() != DialogResult.OK) return;
            txtTarget.Text = sd.FileName;

            double wcl = 0;//完成率
         
            using (Stream fsRead=new FileStream(txtSource.Text,FileMode.Open,FileAccess.Read))
            using (Stream fsWrite = new FileStream(txtTarget.Text, FileMode.Create, FileAccess.Write))
            {
                progressBar1.Maximum = (int)fsRead.Length; //根据读取文件字节大小设置控件最大范围
                byte[] buffer = new byte[1024*1024*1]; //每次读1mb
                int readByteCount = 0;//实际读到的字节数

                while (true)//读取文件流，直到读到的字节数为0时停止
                {
                    readByteCount = fsRead.Read(buffer, 0, buffer.Length); //读
                    fsWrite.Write(buffer, 0, readByteCount);//写

                    progressBar1.Value = (int)fsWrite.Length;//根据已读到的字节数设置进度条数值

                    //设置当前读取的百分比
                    wcl = Convert.ToDouble(fsWrite.Length) / Convert.ToDouble(fsRead.Length) * 100; 
                    label3.Text = ((int)wcl).ToString() + "%";

                    Thread.Sleep(100);//使程序挂起100毫秒，促使在客户端可以看到不断变化的过程（不设置的话程序不大的情况会一闪而过）

                    Application.DoEvents();//重点，必须加上，否则父子窗体都假死

                    if (readByteCount == 0) break; //读取完毕
    
                }

                label3.Text = "100%";

                string debug = string.Empty;

            } // END Using

        }

       


    }
}
