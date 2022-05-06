using Microsoft.EntityFrameworkCore;
using ParcerLibrarry;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Server.Data;
using TestTask.Server.Models;
using TestTask.Shared;

namespace TestTask.Server.Repositories
{
    public class ParcerXmlRepository : IParcerXmlRepository
    {
        private readonly ParcerContext _context;
        public ParcerXmlRepository(ParcerContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(string url, List<LinkTime> linkTimes)
        {
            var link = await _context.Links.FirstOrDefaultAsync(x => x.Name == url);
            if (link != null)
            {
                _context.Links.Remove(link);
                await _context.SaveChangesAsync();
            }

            var parcedLinks = linkTimes.Select(x => new ParcedLink { Name = x.Link, Time = x.Time, Link = link }).ToList();
            link = new Link { Name = url, ParcedLinks = parcedLinks };
            _context.Links.Add(link);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<LinkModel> Get()
        {
            var links = _context.Links.Select(x => new LinkModel { Link = x.Name }).ToList();
            return links;
        }

        public async Task<Link> GetById(int id)
        {
            return await _context.Links.Select(x=>x).Where(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<LinkTime>> Parce(string url)
        {
            var parceHashSet = await ParcerXmlFile.Parce(url);
            var parceList = CreaterListOfLinks.Create(parceHashSet);
            SorterLinkTime.Sort(ref parceList);
            await Create(url, parceList);
            return parceList;
        }
    }
}
