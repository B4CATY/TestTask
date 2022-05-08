using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Shared.ModelsDb
{
    public class Link
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfParce { get; set; } = DateTime.Now;
        public ICollection<ParcedLink> ParcedLinks { get; set; }
    }
}
