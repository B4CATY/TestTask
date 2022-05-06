using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcerLibrarry
{
    public class LinkTime
    {
        private readonly string _link;
        private readonly double _time;

        public string Link { get { return _link; } }
        public double Time { get { return _time; } }

        public LinkTime(string link, double time)
        {
            _link = link;
            _time = time;
        }

    }
}
