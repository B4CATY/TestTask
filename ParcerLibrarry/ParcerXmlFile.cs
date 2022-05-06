using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ParcerLibrarry
{
    public static class ParcerXmlFile
    {

        public static async Task<HashSet<string>> Parce(string url)
        {
            string downloadedXmlFile = await DownloaderXmlFile.DownloadXmlFileDoc(url);

            HashSet<string> xmlHash = new HashSet<string>();
           
            XmlDocument urldoc = new XmlDocument();

            urldoc.LoadXml(downloadedXmlFile);

            XmlNodeList xmlSitemapList = urldoc.GetElementsByTagName("loc");

            foreach (XmlNode node in xmlSitemapList)
                xmlHash.Add(node.InnerText);

            if (xmlHash.Count == 0) 
                throw new Exception("There is not a xml document on this web-site, or a web-site forbade access to him.");

            return xmlHash;
        }
    }
}
