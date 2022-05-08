using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using TestTask.Shared;
using TestTask.Shared.ModelsDb;
using TestTask.Shared.Pagination;
using System.Linq;
using System.Text.Json;

namespace TestTask.Client.Repositories
{
    public class ParcerXmlClientRepository : IParcerXmlClientRepository
    {
        private readonly HttpClient _httpClient;
        public ParcerXmlClientRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<LinkResponseModel> LinksDeserialize(string responseString)
        {
            var links = JsonSerializer.Deserialize<List<LinkResponseModel>>(responseString,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return links;
            //
        }

        public async Task<bool> Parce(LinkModel model)
        {
            var result = await _httpClient.PostAsJsonAsync("ParcerXml", model);
            if (result.IsSuccessStatusCode)
                return true;

            return false;


        }

    }
}
