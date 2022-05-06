using System;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace ParcerLibrarry
{
    public static class DownloaderHtmlFile
    {
        
        public static async Task<string> DownloadHtmlFileDoc(string link)
        {
            if (String.IsNullOrEmpty(link)) 
                link = "https://www.ukad-group.com";

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(link);

            HttpContent content = response.Content;

            return await content.ReadAsStringAsync();
           
        } 
    }
}
