
using ParcerLibrarry;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Shared;
using TestTask.Shared.ModelsDb;
using TestTask.Shared.Pagination;

namespace TestTask.Server.Repositories
{
    public interface IParcerXmlRepository
    {
        Task<List<LinkTime>> ParceAsync(string url);
        Task<bool> CreateAsync(string url, List<LinkTime> linkTimes);
        List<LinkModel> GetAsync();
        /*Task<List<ParcedLink>> GetByIdAsync(PaginationDTO model);*/
        IQueryable<ParcedLink> GetById(PaginationDTO model);
    }
}
