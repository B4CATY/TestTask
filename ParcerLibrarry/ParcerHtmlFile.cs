using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Http;
using System.Diagnostics;
using System.Xml;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace ParcerLibrarry
{
    public static class ParcerHtmlFile
    {

        public static async Task<HashSet<string>> Parce(string url)
        {
            try
            {
                string downloadedhtmlHashFile = await DownloaderHtmlFile.DownloadHtmlFileDoc(url);
                int htmlHashCount = 0;
                

                
                var links = Regex.Matches(downloadedhtmlHashFile, "<a(.*?) href=\"(.*?)\"").Cast<Match>().Select(x => x.Groups[2].Value);


                var uriI = new Uri(url);
                url = Uri.UriSchemeHttps + "://" + uriI.Host;

                HashSet<string> htmlHash = new HashSet<string>();

                #region foreach
                foreach (var item in links)
                    AddLink(item, url, uriI, ref htmlHash);

                var listHtmlHashTemp = htmlHash.ToList();
                #endregion
                int i = 0;
                var htmlHashTemp = new HashSet<string>();

                while (true)
                {
                    htmlHashCount = listHtmlHashTemp.Count;
                    for (; i < htmlHashCount; i++)
                    {
                        var htmlHashDoc = await DownloaderHtmlFile.DownloadHtmlFileDoc(listHtmlHashTemp[i]);
                        links = Regex.Matches(htmlHashDoc, "<a(.*?) href=\"(.*?)\"").Cast<Match>().Select(x => x.Groups[2].Value);

                        foreach (var itemshtmlHash in links)
                            AddLink(itemshtmlHash, url, uriI, ref htmlHashTemp);
                    }
                    htmlHash.UnionWith(htmlHashTemp);
                    listHtmlHashTemp = htmlHash.ToList();
                    if (htmlHashCount == htmlHash.Count)
                        break;
                }

                
                if (htmlHash.Count == 0) throw new Exception("Ban htmlHash document");

                return htmlHash;
            }
            catch (ArgumentNullException ex)
            {
                
                return null;
            }
            catch (WebException)
            {
                
                return null;
            }
            catch (UriFormatException ex)
            {
                
                return null;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }




        private static void AddLink(string item, string url, Uri uriI, ref HashSet<string> htmlHash)
        {
            try
            {
                
                if (item.Contains(" ")|| item.Contains("/#")|| item.Contains("/?"))
                    return;

                else if (item.Contains("https") || item.Contains("http") || item.Contains("www"))
                {
                    string tempItem;
                    if (item[0] == '/' && item[1] == '/')
                    {
                        tempItem = "https:" + item;
                        var tempIri = new Uri(tempItem);

                        if (uriI.Host == tempIri.Host)
                            htmlHash.Add(tempItem);
                    }
                    else
                    {
                        var tempIri = new Uri(item);
                        if (uriI.Host == tempIri.Host)
                            htmlHash.Add(item);

                    }

                }

                else if (item[0] == '/' || item.Contains(url))
                {

                    if (item.Contains(url) || item.Contains(uriI.Host))
                        htmlHash.Add(item);
                    else
                        htmlHash.Add(url + item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
