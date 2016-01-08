using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMLExtracter
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONHelper s = new JSONHelper();
            SharePointHelper sh = null;
            System.Net.NetworkCredential cred = null;
            if (Settings.Instance.Password.Length > 2)
            {
                cred = new System.Net.NetworkCredential(Settings.Instance.Username, Settings.Instance.Password);
                sh = new SharePointHelper(Settings.Instance.SharePointUrl, cred);
            }
            else
                sh = new SharePointHelper(Settings.Instance.SharePointUrl);

            s.StoreFolder = Settings.Instance.StoreFolder;

            string action = args[0].ToLower();
            if (action == "file")
            {
                RecreateList(sh);
                s.ReadSingleFile(args[1],sh);
            }
            else if (action == "folder")
                s.ReadSingleFolder(args[1]);
            else if (action == "setupsp")
                RecreateList(sh);
            else
                PrintInfo();

        }

        private static void RecreateList(SharePointHelper sh)
        {
            sh.DeleteDocumentLibrary("Reuter");
            List list = sh.CreateDocumentLibrary("Reuter", "ReuterFiles");
            TermGroup group = sh.CreateGroup(sh.GetTermStore(), "MachineLearning");
            TermSet set = sh.CreateTermSet(group, "MachineLearning");
            sh.DeleteFieldIfExists("Topic");
            Microsoft.SharePoint.Client.Field taxField = sh.CreateTaxonomyField(sh.GetTermStore(), set, "Topic", "Topic", false, false);
            sh.AddTaxFieldToList(list, taxField, true);
        }

        private static void CheckLoadConfiguration()
        {
            

        }
       
        static void PrintInfo()
        {
            Console.WriteLine(@"Program file c:\Data\data.sgml");
            Console.WriteLine(@"Program folder c:\Data");
            Console.WriteLine(@"Program setupsp c:\Data UserName Password");
        }
    }
}
