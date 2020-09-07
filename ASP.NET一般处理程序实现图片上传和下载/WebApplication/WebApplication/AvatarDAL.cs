using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
namespace WebApplication
{

    /// <summary>
    /// 头像信息数据访问类
    /// </summary>
    public class AvatarDAL
    {

        public XmlDocument XmlDoc { get; set; }
        public string XmlPath { get; set; }
        public AvatarDAL(string path)
        {
            XmlDoc = new XmlDocument();
            XmlDoc.Load(path);
            XmlPath = path;
        }

        /// <summary>
        /// 创建一个头像信息数据节点
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="path">头像所在路径</param>
        public void CreateAvatarNode(AvatarInfo data)
        {
            XmlElement item = XmlDoc.CreateElement("Avatar");
 

            if (!string.IsNullOrEmpty(data.Name))
            {
                XmlAttribute attrName = XmlDoc.CreateAttribute("Name");
                attrName.Value = data.Name;
                item.Attributes.Append(attrName);
            }

            if (!string.IsNullOrEmpty(data.Path))
            {
                XmlAttribute attrPath = XmlDoc.CreateAttribute("Path");
                attrPath.Value = data.Path;
                item.Attributes.Append(attrPath);
            }

            if (!string.IsNullOrEmpty(data.ID))
            {
                XmlAttribute attrID = XmlDoc.CreateAttribute("ID");
                attrID.Value = data.ID;
                item.Attributes.Append(attrID);
            }

            if (!string.IsNullOrEmpty(data.HttpContentType))
            {
                XmlAttribute attrHttpContentType = XmlDoc.CreateAttribute("HttpContentType");
                attrHttpContentType.Value = data.HttpContentType;
                item.Attributes.Append(attrHttpContentType);
            }

            XmlDoc.DocumentElement.AppendChild(item);
            XmlDoc.Save(XmlPath);
        }// END CreateAvatarNode（）


        /// <summary>
        /// 获取所有头像信息
        /// </summary>
        /// <returns></returns>
        public List<AvatarInfo> GetAvatarInfo()
        {
            List<AvatarInfo> list = new List<AvatarInfo>();

           XmlNodeList Avatares=  XmlDoc.GetElementsByTagName("Avatar");
            
           foreach (XmlNode node in Avatares)
           {
               string name = node.Attributes["Name"].Value;
               string path = node.Attributes["Path"].Value;
               string id = node.Attributes["ID"].Value;
               string httpContentType = node.Attributes["HttpContentType"].Value;
               list.Add(new AvatarInfo() { Name = name, Path = path, HttpContentType = httpContentType, ID=id });
           }

            return list;
        }  // END GetAvatarInfo（）


        public AvatarInfo GetAvatarInfoByID(string id)
        {
            AvatarInfo data = new AvatarInfo();

            XmlNode node = XmlDoc.SelectSingleNode("//Avatar[@ID='" + id + "']");


            data.ID = node.Attributes["ID"].Value;
            data.Path = node.Attributes["Path"].Value;
            data.HttpContentType = node.Attributes["HttpContentType"].Value;
           
 
            return data;
        } // END GetAvatarInfoByID（）



    }
}