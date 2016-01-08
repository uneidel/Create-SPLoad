//using Microsoft.SharePoint.Client.Utilities;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Xml;

//namespace SGMLExtracter
//{
//    internal class SGMLHelper
//    {
//        Sgml.SgmlReader sgml = null;
//        HashSet<String> topicset = new HashSet<string>();
//        internal SGMLHelper()
//        {
//            sgml = new Sgml.SgmlReader();

//        }
//        public string StoreFolder { get; set; }
//        internal void ReadSingleFile(TextReader str, string DocType = "SGML")
//        {
//            sgml.DocType = DocType;
//            sgml.IgnoreDtd = true;
//            sgml.InputStream = str;

//            XmlDocument x = new XmlDocument();
//            x.Load(sgml);
            
//            XmlNodeList nodes = x.SelectNodes("root/REUTERS");

//            foreach (XmlNode node in nodes)
//            {
//                var foo = node.InnerXml;
//                var topic = node.SelectSingleNode("TOPICS");
                
//                if (!String.IsNullOrEmpty(topic.InnerText))
//                 topicset.Add(topic.InnerText); //TODO Multiple Topics 
//                foreach (XmlNode cn in topic.ChildNodes)
//                    topicset.Add(cn.InnerText);

//                WordHelper wh = new WordHelper();
//                byte[] file = null;
//                if ( ( file =wh.CreateWordDocumentByReuterFile(node))!= null)
//                { 
//                    File.WriteAllBytes(Path.Combine(StoreFolder, Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".docx", file );
                   
//                }
//            }

//        }
//        internal void ReadSingleFile(string filePath, string DocType = "SGML")
//        {
//            using (TextReader reader = File.OpenText(filePath))
//            {
//                //nasty Workaround 
//                MemoryStream stream = new MemoryStream();
//                StreamWriter writer = new StreamWriter(stream);
//                writer.Write("<root>" + StripOutInvalidCharacters(reader.ReadToEnd()) + "</root>");
//                stream.Flush();
//                stream.Position = 0;
                
//                ReadSingleFile(new StreamReader(stream));
//            }
//        }

//        internal void ReadSingleFolder(string folder)
//        {
//            foreach (var file in Directory.GetFiles(folder, "*.sgm"))
//            {
//                ReadSingleFile(file);
//            }

//        }

//        internal string StripOutInvalidCharacters(string text)
//        {
             
//            var tx=   Regex.Replace(text, @"([&][#]\d+[;])+", "", RegexOptions.Multiline);
//            return tx;

//        }
//    }
//}