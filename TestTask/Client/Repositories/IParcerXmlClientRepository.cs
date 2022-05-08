using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Shared;
using TestTask.Shared.ModelsDb;
using TestTask.Shared.Pagination;

namespace TestTask.Client.Repositories
{
    public interface IParcerXmlClientRepository
    {
        List<LinkResponseModel> LinksDeserialize(string responseString);
        Task<bool> Parce(LinkModel model);

    }
}
