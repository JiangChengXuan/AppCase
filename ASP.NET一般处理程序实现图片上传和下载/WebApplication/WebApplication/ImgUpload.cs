using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Reflection;
using System.IO;
namespace WebApplication
{

    /// <summary>
    /// 处理图片上传
    /// </summary>
    public class ImgUpload
    {

        public ImgUpload(HttpPostedFile file)
        {
            this.HttpPostFile = file;
            this.ImgTypeList=LoadImgTypeList();
            CreateSavePath();
        }

        public ImgUpload(HttpPostedFile file,string savePath)
        {
            this.HttpPostFile = file;
            this.SavePath=savePath;
            this.ImgTypeList = LoadImgTypeList();
        }

        public HttpPostedFile HttpPostFile { get; set; }

        private int _fileLengthMax;

        /// <summary>
        /// 文件上传大小限制，默认为10MB
        /// </summary>
        public int FileLengthMax
        {
            get
            {
                int length = ConfigurationManager.AppSettings["maxRequestLength"] == null ?
                    10240 : Convert.ToInt32(ConfigurationManager.AppSettings["maxRequestLength"]);

                _fileLengthMax = length;
                return _fileLengthMax;
            }

        }

        /// <summary>
        /// 图片类型集合
        /// </summary>
        public List<string> ImgTypeList { get; set; }


        /// <summary>
        /// 图片保存路径
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 响应输出给浏览器的上下文类型
        /// </summary>
        public string HttpContentType { get; set; }


        /// <summary>
        /// 创建图片保存的路径
        /// </summary>
        private void CreateSavePath()
        {
            if (!string.IsNullOrEmpty(this.SavePath)) return;

            string fileName = Guid.NewGuid()+"_"+HttpPostFile.FileName;
            string saveDire = HttpContext.Current.Server.MapPath("/")+"Images/"+DateTime.Now.ToString("yyyy-MM-dd");

            //判断图片上传的目录是否存在，不存在则创建
            if (!Directory.Exists(saveDire))
            {
                Directory.CreateDirectory(saveDire);
            }

            this.SavePath= Path.Combine(saveDire, fileName);

        } // END CreateSavePath（）


        /// <summary>
        /// 加载可以上传的图片文件类型
        /// </summary>
        private List<string> LoadImgTypeList()
        {
            List<string> list = new List<string>();
            Type type = typeof(ImgType);
            FieldInfo [] fields=type.GetFields();

            foreach (FieldInfo field in fields)
            {
                if (!field.IsSpecialName)
                {
                    string imgType = field.Name;
                    list.Add(imgType);
                }
            }

            return list;
        } // END LoadImgTypeList（）


        /// <summary>
        /// 对上传文件进行判断
        /// </summary>
        /// <param name="file">HttpPostedFile</param>
        /// <param name="errMsg">错误消息</param>
        /// <returns></returns>
        private bool UploadCheck(out string errMsg)
        {
            errMsg = string.Empty;

            //文件大小判断
            if (HttpPostFile.ContentLength <= 0)
            {
                errMsg = "不支持上传空文件";
                return false;
            }

            if (HttpPostFile.ContentLength/1024 > FileLengthMax)
            {
                errMsg = string.Format("文件不能超过{0}MB", FileLengthMax / 1024);
                return false;
            }

            string extName = Path.GetExtension(HttpPostFile.FileName).TrimStart('.').ToLower();
            HttpContentType = "image/" + extName;

            //如果文件扩展名不在上传范围内或者ContentType不是Image，则禁止上传
            if (!ImgTypeList.Contains(extName) || !HttpPostFile.ContentType.Contains("image"))
            {
                errMsg = "上传文件必须是图片";
                return false;
            }

            return true;
        } // END UploadCheck（）


        /// <summary>
        /// 图片上传
        /// </summary>
        public bool Upload(out string errMsg)
        {
            errMsg = string.Empty;

            if (!UploadCheck(out errMsg))
            {
                return false;
            }

            Stream postStream = null;
            FileStream fsWrite = null;
            try
            {
                postStream = HttpPostFile.InputStream;
                fsWrite = new FileStream(this.SavePath, FileMode.Create, FileAccess.Write);
                byte[] buffer = new byte[1024 * 1024]; // 1次读1mb

                int readCount = 0;
                while ((readCount = postStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fsWrite.Write(buffer, 0, readCount);
                }
            }
            finally
            {
                if (postStream != null) postStream.Dispose();
                if (fsWrite != null) fsWrite.Dispose();
            }

            return true;
        }  // END Upload（）




    }
}