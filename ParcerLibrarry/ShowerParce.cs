using System;
using System.Collections.Generic;
using System.Linq;

namespace ParcerLibrarry
{
    public static class ShowerParce
    {
        
        public static void Run(HashSet<string> xmlHashSet, HashSet<string> htmlHashSet)
        {
            HashSet<string> hashSetOfIdenticalLinks = LinkSeparator.FindCommonLinks(xmlHashSet, htmlHashSet);

            LinkSeparator.ToGetDifferentReferencesOnly(ref xmlHashSet,ref htmlHashSet);// takes away identical elements in a list

            var listXml = CreaterListOfLinks.Create(xmlHashSet);//carries from hash in list and also knows time of response
            var listHtml = CreaterListOfLinks.Create(htmlHashSet);//carries from hash in list and also knows time of response
            var listOfIdenticalLinks =  CreaterListOfLinks.Create(hashSetOfIdenticalLinks);

            var listAll = listHtml.Concat(listXml).Concat(listOfIdenticalLinks).ToList();
            SorterLinkTime.Sort(ref listAll);

            Console.WriteLine("Press any key");
            Console.ReadKey();
            Console.Clear();
            int i = 1;
            Console.WriteLine("If time = -1, site cant be opened");
            if (listXml.Count == 0)
                Console.WriteLine("in sitemap 0 links or sitemap cant be opened");
            else
            {
                Console.WriteLine($"\n\n\nUrls FOUNDED IN SITEMAP.XML but not founded after crawling a web site \n\n");
                
                foreach (var item in listXml)
                {
                    Console.WriteLine($"{i}){item.Link}");
                    i++;
                }
            }
            Console.WriteLine("\n\n\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml\n\n");
            if (listHtml.Count == 0)
                Console.WriteLine("in html 0 links or html cant be opened");
            else
            {
                i = 1;
                foreach (var item in listHtml)
                {
                    Console.WriteLine($"{i}){item.Link}");
                    i++;
                }
            }
            i = 1;
            
            
            Console.WriteLine("\n\n\n\n\nTiming\nUrl\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tTiming(ms)");
            foreach (var item in listAll)
            {
                Console.WriteLine($"{i}){item.Link, -135}{item.Time, 13}");
                i++;
            }
            

            Console.WriteLine($"\n\nUrls(html documents) found after crawling a website: {listOfIdenticalLinks.Count + listHtml.Count}");
            Console.WriteLine($"\n\nUrls found in sitemap: {listOfIdenticalLinks.Count + listXml.Count}");
            Console.WriteLine($"\n\nUrls list All Max: {listAll.Max(x => x.Time)}");
            Console.WriteLine($"\n\nUrls list All Min: {listAll.Min(x => x.Time)}"); ;



        }
    }
}
