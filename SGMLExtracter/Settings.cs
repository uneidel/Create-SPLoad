using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGMLExtracter
{
    internal class Settings
    {
        private static Settings instance;
        string sharepointUrl, username, password, doclibName, storefolder = String.Empty;
        int lcid;
        private Settings()
        {
            sharepointUrl = System.Configuration.ConfigurationManager.AppSettings["SharePointUrl"];
            username = System.Configuration.ConfigurationManager.AppSettings["Username"];
            password = System.Configuration.ConfigurationManager.AppSettings["Password"];
            doclibName = System.Configuration.ConfigurationManager.AppSettings["SharePointUrl"];
            storefolder = System.Configuration.ConfigurationManager.AppSettings["StoreFolder"];
            lcid = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["LCID"]);
        }
        public string SharePointUrl
        {
            get { return sharepointUrl; }
        }
        public string StoreFolder
        {
            get { return storefolder; }
        }
        public string Username
        {
            get { return username; }
        }
        public string Password
        {
            get { return password; }
        }
        public string DocLibName
        {
            get { return doclibName; }
        }
        public int LCID
        {
            get { return lcid; }
        }
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Settings();
                }
                return instance;
            }
        }

    }
}
