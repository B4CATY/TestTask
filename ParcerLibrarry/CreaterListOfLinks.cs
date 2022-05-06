using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcerLibrarry
{
    public static class CreaterListOfLinks
    {
        public static List<LinkTime> Create(HashSet<string> hashSetOfLink)
        {
            List<LinkTime> listOfLinks = new List<LinkTime>();
            foreach (var item in hashSetOfLink)
                listOfLinks.Add(new LinkTime(item, NoterTheTimeResponse.Run(item)));

            SorterLinkTime.Sort(ref listOfLinks);
            return listOfLinks;
            
        }
    }
}
