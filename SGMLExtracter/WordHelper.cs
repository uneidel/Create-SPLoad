using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace SGMLExtracter
{
    internal class WordHelper
    {
        public byte[] CreateWordDocumentByReuterFile(Entry e)
        {
            byte[] file = null;
            
            using (MemoryStream ms = new MemoryStream())
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document, true))
                {
                    // Add a main document part. 
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                    // Create the document structure and add some text.
                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());
                    Paragraph para = body.AppendChild(new Paragraph());
                    Run run = para.AppendChild(new Run());
                    run.AppendChild(new Text(e.Title));
                   
                    run.AppendChild(new Break());
                    para.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId() { Val = "Heading1" });

                    Paragraph para1 = body.AppendChild(new Paragraph());
                    Run run1 = para.AppendChild(new Run());
                    run.AppendChild(new Text(FixInvalidCharacter(e.Body)));
                    run.AppendChild(new Break());
                    para1.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId() { Val = "Heading1" });
                }
                file = ms.ToArray();
            }
            return file;
        }
        string FixInvalidCharacter(string tx)
        {
            if (tx != null)
            {
                var _rx = new Regex(@"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF]", RegexOptions.Compiled);
                return _rx.Replace(tx, string.Empty);
            }
            else
                return String.Empty;
        }


        
        private ReuterContent ParseNode(XmlNode node)
        {

            try
            { 
            var Title = TraverseNodes(node.ChildNodes, "TITLE").InnerText;
            var Body = TraverseNodes(node.ChildNodes, "BODY").InnerText;
            var Date = TraverseNodes(node.ChildNodes, "DATE").InnerText;
            var Dateline = TraverseNodes(node.ChildNodes, "DATELINE").InnerText;
            return new ReuterContent()
            {
                Title = Title,
                Body = Body,
                Date = Date,
                Dateline = Dateline
            };
               
            }
            catch
            {
                return null;
            }

        }

        private XmlNode TraverseNodes(XmlNodeList nodes, string nodeName)
        {
            XmlNode targetnode = null;
            foreach (XmlNode node in nodes)
            {
                if (node.Name == nodeName)
                { 
                    targetnode=  node;
                    break;
                }

                targetnode = TraverseNodes(node.ChildNodes, nodeName);
            }
            return targetnode;
        }
        public class ReuterContent
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Dateline { get; set; }
            public string Date { get; set; }
        }

    }
}
