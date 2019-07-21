using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            // writeXML1();
            // writeXML2();
            //Insert();
            //Modify();
            //Delete();
            ShowAll();
        }
        //创建xml文件

        public static void WriteXML1()
        {
            XmlTextWriter writer = new XmlTextWriter("titles.xml", null);      //写入根元素 
            writer.WriteStartElement("items");      //加入子元素 
            writer.WriteElementString("title", "Unreal Tournament 2003");
            writer.WriteElementString("title", "C&C: Renegade");
            writer.WriteElementString("title", "Dr. Seuss's ABC");      //关闭根元素，并书写结束标签 
            writer.WriteEndElement();      //将XML写入文件并且关闭XmlTextWriter     
            writer.Close();

        }
        //创建xml文件
        public static void WriteXML2()
        {
            XmlTextWriter writer1 = new XmlTextWriter("myMedia.xml", null);
            //使用自动缩进便于阅读   
            writer1.Formatting = Formatting.Indented;
            //书写根元素   
            writer1.WriteStartElement("items");
            //开始一个元素     
            writer1.WriteStartElement("item");
            //向先前创建的元素中添加一个属性     
            writer1.WriteAttributeString("rating", "R");
            //添加子元素     
            writer1.WriteElementString("title", "The Matrix");
            writer1.WriteElementString("format", "DVD");
            //关闭item元素     
            writer1.WriteEndElement();
            //在节点间添加一些空格    
            writer1.WriteWhitespace("\n");
            //使用原始字符串书写第二个元素    
            writer1.WriteRaw("<item>" + "<title>BloodWake</title>" + "<format>XBox</format>" + "</item>");
            //使用格式化的字符串书写第三个元素    
            writer1.WriteRaw("\n  <item>\n" + "    <title>Unreal Tournament 2003</title>\n" + "    <format>CD</format>\n" + "  </item>\n");
            // 关闭根元素    
            writer1.WriteFullEndElement();
            //将XML写入文件并关闭writer      
            writer1.Close();
        }
        //插入节点
        public static void Insert()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("bookstore.xml");
            //查找<bookstore>  
            XmlNode root = xmlDoc.SelectSingleNode("bookstore");
            //创建一个<book>节点    
            XmlElement xe1 = xmlDoc.CreateElement("book");
            //设置该节点genre属性
            xe1.SetAttribute("genre", "李赞红");
            //设置该节点ISBN属性    
            xe1.SetAttribute("ISBN", "2-3631-4");
            //设置文本节点    
            XmlElement xesub1 = xmlDoc.CreateElement("title");
            xesub1.InnerText = "CS从入门到精通";
            //添加到<book>节点中
            xe1.AppendChild(xesub1);
            XmlElement xesub2 = xmlDoc.CreateElement("author");
            xesub2.InnerText = "候捷";
            xe1.AppendChild(xesub2);
            XmlElement xesub3 = xmlDoc.CreateElement("price");
            xesub3.InnerText = "58.3";
            xe1.AppendChild(xesub3);
            //添加到<bookstore>节点中
            root.AppendChild(xe1);

            xmlDoc.Save("bookstore.xml");

        }
        //修改节点
        public static void Modify()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("bookstore.xml");
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("bookstore").ChildNodes;//获取bookstore节点的所有子节点    
            foreach (XmlNode xn in nodeList)//遍历所有子节点   
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型     
                if (xe.GetAttribute("genre") == "李赞红")//如果genre属性值为“李赞红”    
                {
                    xe.SetAttribute("genre", "update李赞红");//则修改该属性为“update李赞红”
                    XmlNodeList nls = xe.ChildNodes;//继续获取xe子节点的所有子节点    
                    foreach (XmlNode xn1 in nls)//遍历    
                    {
                        XmlElement xe2 = (XmlElement)xn1;//转换类型    
                        if (xe2.Name == "author")//如果找到    
                        {
                            xe2.InnerText = "亚胜";//则修改      
                            break;//找到退出来就可以了      
                        }
                    }
                    break;
                }
            }
            xmlDoc.Save("bookstore.xml");//保存。
        }

        //删除节点
        public static void Delete()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("bookstore.xml");
            XmlNodeList xnl = xmlDoc.SelectSingleNode("bookstore").ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("genre") == "fantasy")
                {
                    xe.RemoveAttribute("genre");//删除genre属性
                }
                else if (xe.GetAttribute("genre") == "update李赞红")
                {
                    xe.RemoveAll();//删除该节点的全部内容     }  
                }
                xmlDoc.Save("bookstore.xml");
            }
        }
        //显示节点文本
        public static void ShowAll()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("bookstore.xml");
            XmlNode xn = xmlDoc.SelectSingleNode("bookstore");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xnf in xnl)
            {
                XmlElement xe = (XmlElement)xnf;
                Console.WriteLine(xe.GetAttribute("genre"));//显示属性值    
                Console.WriteLine(xe.GetAttribute("ISBN"));
                XmlNodeList xnf1 = xe.ChildNodes;
                foreach (XmlNode xn2 in xnf1)
                {
                    if ("title".Equals(xn2.Name))
                    {
                        Console.WriteLine(xn2.InnerText);//显示子节点点文本  
                    }
                      
                }
            }
            Console.ReadLine();
        }

    }
}
