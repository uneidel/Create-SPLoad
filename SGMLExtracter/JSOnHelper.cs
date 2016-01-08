using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMLExtracter
{
    internal class JSONHelper
    {
        Sgml.SgmlReader sgml = null;
        HashSet<String> topicset = new HashSet<string>();
        internal JSONHelper()
        {
            

        }
        public string StoreFolder { get; set; }
       
        internal void ReadSingleFile(string filePath, SharePointHelper sh,string DocType = "SGML")
        {

            using (StreamReader streamReader = new StreamReader(filePath))
            {

                string text = streamReader.ReadToEnd();
                var entries = JsonConvert.DeserializeObject<List<Entry>>(text);
                foreach(Entry e in entries)
                {
                    WordHelper wh = new WordHelper();
                    byte[] file = wh.CreateWordDocumentByReuterFile(e);
                    
                    sh.UploadDocument("Reuter", "/", GetSampleWordName(), file);

                }
            }
        }
        internal string GetSampleWordName()
        {
            return String.Format("{0}.docx", Path.GetFileNameWithoutExtension(Path.GetTempFileName()));

        }
        internal void ReadSingleFolder(string folder)
        {


        }
    }
    public class Entry
    {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "topics")]
        public string[] Topics { get; set; }

        [JsonProperty(PropertyName = "places")]
        public string[] Places { get; set; }


    }
}