

using System.Text.Json.Serialization;

namespace TestTask.Shared.ModelsDb
{
    public class ParcedLink
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int LinkId { get; set; }

        public string Name { get; set; }
        public double Time { get; set; }
        [JsonIgnore]
        public Link Link { get; set; }
    }
}
