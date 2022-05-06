
using System.Collections.Generic;

namespace TestTask.Server.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ParcedLink> ParcedLinks { get; set; }
    }
}
