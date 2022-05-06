using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcerLibrarry
{
    public static class LinkSeparator
    {

        public static void ToGetDifferentReferencesOnly(ref HashSet<string> xmlHashSet, ref HashSet<string> htmlHashSet)
        {
            HashSet<string> temp = new HashSet<string>(xmlHashSet); //xmlHashSet

            xmlHashSet.ExceptWith(htmlHashSet); //xmlHashSet     htmlHashSet
            htmlHashSet.ExceptWith(temp); //htmlHashSet
        }
        public static HashSet<string> FindCommonLinks(HashSet<string> xmlHashSet, HashSet<string> htmlHashSet)
        { 
            HashSet<string>  listOfIdenticalLinks = new HashSet<string>(xmlHashSet); //xmlHashSet
            listOfIdenticalLinks.IntersectWith(htmlHashSet); //htmlHashSet
            return listOfIdenticalLinks;
        }
    }
}
