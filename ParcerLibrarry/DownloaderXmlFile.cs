using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParcerLibrarry
{
    public static class DownloaderXmlFile
    {
        public static async Task<string> DownloadXmlFileDoc(string link)
        {
            
            if (String.IsNullOrEmpty(link)) link = "https://www.ukad-group.com";

            var uriI = new Uri(link);
            link = Uri.UriSchemeHttps + "://" + uriI.Host + "/sitemap.xml";

            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                return await wc.DownloadStringTaskAsync(link);
            }
            catch (ArgumentNullException ex)
            {
                
                return null;
            }
            catch (WebException ex)
            {
                
                return null;
            }


        }
    }
}
