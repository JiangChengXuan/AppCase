using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Drawing.Drawing2D;
namespace GDI_生成验证码
{
    public class ValidateCode
    {
          public   ValidateCode()
          {
              this.Height = 50;
           }

        /// <summary>
        /// 验证码中每个码的像素
        /// </summary>
        private const int CodePixel = 25;
 

        /// <summary>
        /// 验证码图片高度
        /// </summary>
        public int Height { get; set; }



        //验证码
        private string _code;

        /// <summary>
        /// 验证码
        /// </summary>
        public string  Code
        {
            get 
            {
                string guid = Guid.NewGuid().ToString().Replace("-","");
                Random random = new Random();

                for (int i = 0; i < 4; i++) //验证码默认长度为4
                {
                  int index=  random.Next(guid.Length);
                  _code += guid[index];
                }
                return _code;
            }
        }

        public Image DrawCodeImg()
        {
 
                //Step1-画框
            Bitmap image = new Bitmap(100, Height);

                //Step2-画板
            Graphics grap = Graphics.FromImage(image);
                grap.Clear(Color.White);

                #region Step3-绘制验证码

                Font font = new Font("微软雅黑", CodePixel); //字体
                Point point = new Point(5, 3); //绘制的位置

                //线性渐变刷子，使验证码有渐变色的效果
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
                LinearGradientBrush linGrBrush = new LinearGradientBrush(rect, Color.Red, Color.Blue, LinearGradientMode.Horizontal);

                //绘
                grap.DrawString(Code, font, linGrBrush, point);

                #endregion

                #region Step4-绘制干扰

                //绘制干扰线
                CreateDistractionLines(grap, image);

                //创建干扰点
                CreateDistractionPoint(grap, image);

                #endregion
          
           

            return image;
        }  // END CreateCode（）


        /// <summary>
        /// 创建干扰点
        /// </summary>
        /// <param name="grap"></param>
        /// <param name="image"></param>
        private void CreateDistractionPoint(Graphics grap, Image image)
        {
            Random random = new Random();
            Pen pen = new Pen(Color.FromArgb(34, 139, 34), 2);

            for (int i = 0; i < 60; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                Point point1 = new Point(x, y);
                Point point2 = new Point(x+1, y+1);
                grap.DrawLine(pen, point1, point2);
            }


        }   // END CreateDistractionPoint（）


        /// <summary>
        /// 创建干扰线
        /// </summary>
        /// <param name="grap">画布</param>
        private void CreateDistractionLines(Graphics grap,Image image)
        {
            Random random = new Random();
            List<Point> points = new List<Point>();

            for (int i = 0; i < 20; i++)
            {
                Point point = new Point(random.Next(image.Width), random.Next(image.Height));
                points.Add(point);
            }

            Pen pen = new Pen(Color.FromArgb(192, 192, 192));
            grap.DrawLines(pen, points.ToArray());

        }// END CreateDistractionLines（）


    }
}