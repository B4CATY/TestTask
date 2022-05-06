
namespace TestTask.Server.Models
{
    public class ParcedLink
    {
        public int Id { get; set; }
        public int LinkId { get; set; }

        public string Name { get; set; }
        public double Time { get; set; }

        public Link Link { get; set; }
    }
}
