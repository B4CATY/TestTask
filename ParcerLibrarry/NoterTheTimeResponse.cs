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
    public static class NoterTheTimeResponse
    {
        public static double Run(string link)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            Stopwatch timer = new Stopwatch();

            timer.Start();
            try
            {
                HttpWebResponse response1 = (HttpWebResponse)request.GetResponse();
                response1.Close();

                timer.Stop();
                return timer.Elapsed.TotalMilliseconds;
            }
            catch (WebException ex)
            {
              
                
                
                timer.Stop();
                return -1;
            }
           
            catch (Exception ex)
            {
                
                timer.Stop();
                return -1;
            }
            
            
        }
    }
}
