using ParcerLibrarry;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Server.Models;
using TestTask.Shared;

namespace TestTask.Server.Repositories
{
    public interface IParcerXmlRepository
    {
        Task<List<LinkTime>> Parce(string url);
        Task<bool> Create(string url, List<LinkTime> linkTimes);
        List<LinkModel> Get();
        Task<Link> GetById(int id);
    }
}
