using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //获取小说所在目录路径
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string storyDireName = "福尔摩斯探案集_柯南 ▪ 道尔";
            string storyDirePath = Path.Combine(appPath, storyDireName);

            //使用小说文件夹名称作为根节点
            TreeNode rootNode = treeView1.Nodes.Add(storyDireName);

            BingdingTreeView(rootNode, storyDirePath);
 

        }

        private void BingdingTreeView(TreeNode node, string direPath)
        {
            //获取指定目录下的文件夹和文件
            string[] dires = Directory.GetDirectories(direPath); //文件夹路径集合
            string[] files = Directory.GetFiles(direPath);//文件路径集合


            foreach (string dire in dires)//绑定文件夹
            {
                string direName = Path.GetFileName(dire);
                TreeNode subNode = node.Nodes.Add(direName);

                BingdingTreeView(subNode, dire); //递归
            }

            foreach (string file in files)//绑定文件
            {
                string fileName = Path.GetFileName(file);   
                TreeNode subNode = node.Nodes.Add(fileName);
                subNode.Tag = file;  //文件路径关联
            }

        }// END BingdingTreeView（）
 

  
 

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
            if (e.Node.Tag == null) return;

            string filePath = e.Node.Tag.ToString();
            StringBuilder sb = new StringBuilder();

            using (Stream fsRead = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 5]; //每次读5kb
                int readByteCount = 0; //实际读到的字节数
                while ((readByteCount = fsRead.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string context = Encoding.Default.GetString(buffer, 0, readByteCount);
                    sb.Append(context);
                }
            } // END Using

            textBox1.Text = sb.ToString();
        } 

    }
}
