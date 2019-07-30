using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TraceabilityNew
{
    public class XmlManager
    {
        Hashtable hash_config;
        public string sFilePath;

        public XmlManager(Hashtable obj,string sPath)
        {
            hash_config = obj;
            this.sFilePath = sPath;
        }

        public bool checkPartnumber(string sPartnumber)
        {
            XPathNavigator nav;
            XPathDocument docNav;
            string xPath;

            try
            {

                docNav = new XPathDocument(this.sFilePath);
                nav = docNav.CreateNavigator();
                xPath = "//Partnumber[@name='"+sPartnumber+"']";
        
                string value = nav.SelectSingleNode(xPath).Value;

                return true;

            }catch(NullReferenceException ex)
            {
                // MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void getInfo(string sPartnumber)
        {
           
            int i;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.sFilePath);
            XmlNodeList PictureNodes = xmlDoc.SelectNodes("//Partnumber[@name='"+sPartnumber+"']");

            foreach (XmlNode PictureNode in PictureNodes) 
            {
                for (i = 0; i < PictureNode.ChildNodes.Count; i++)
                {
                    //MessageBox.Show(PictureNode.ChildNodes.Item(i).InnerText);

                    switch (i)
                    {
                        case 0:
                            hash_config.Add("ReadSerialport", PictureNode.ChildNodes.Item(i).InnerText);
                            break;
                        case 1:
                            hash_config.Add("label_check", PictureNode.ChildNodes.Item(i).InnerText);
                            break;
                        case 2:
                            hash_config.Add("label_length", PictureNode.ChildNodes.Item(i).InnerText);
                            break;
                        case 3:
                            hash_config.Add("label1", PictureNode.ChildNodes.Item(i).InnerText);    
                            break;
                        case 4:
                            hash_config.Add("label2", PictureNode.ChildNodes.Item(i).InnerText);
                            break;
                        case 5:
                            hash_config.Add("label3", PictureNode.ChildNodes.Item(i).InnerText);
                            break;
                        case 6:
                            hash_config.Add("label4", PictureNode.ChildNodes.Item(i).InnerText);
                            break;
                        case 7:
                            hash_config.Add("label_MAC", PictureNode.ChildNodes.Item(i).InnerText);
                            break;
                        case 8:
                            hash_config.Add("print_command", PictureNode.ChildNodes.Item(i).InnerText);
                            break;

                    }
                    
                }
              
            } 
    


        }

        public void test2()
        {
            
        }

    }
}
