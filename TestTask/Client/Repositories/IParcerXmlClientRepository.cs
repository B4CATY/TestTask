using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Shared;
using TestTask.Shared.ModelsDb;
using TestTask.Shared.Pagination;

namespace TestTask.Client.Repositories
{
    public interface IParcerXmlClientRepository
    {
        Task<List<ParcedLink>> GetLinkByIdAsync(PaginationDTO model);
        List<LinkResponseModel> LinksDeserialize(string responseString);
        Task<bool> Parce(LinkModel model);

    }
}
