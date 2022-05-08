using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParcerLibrarry;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Server.Data;
using TestTask.Shared;
using TestTask.Shared.ModelsDb;
using TestTask.Shared.Pagination;

namespace TestTask.Server.Repositories
{
    public class ParcerXmlRepository : IParcerXmlRepository
    {
        private readonly ParcerContext _context;
        public ParcerXmlRepository(ParcerContext context) => _context = context;

        public async Task<bool> CreateAsync(string url, List<LinkTime> linkTimes)
        {
            var link = new Link { Name = url };
            var parcedLinks = linkTimes.Select(x => new ParcedLink { Name = x.Link, Time = x.Time, Link = link }).ToList();

            await _context.ParcedLinks.AddRangeAsync(parcedLinks);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<LinkResponseModel> GetLinks()
        {
            var links = _context.Links
                     .Include(x => x.ParcedLinks)
                     .Select(x => new LinkResponseModel
                     {
                         MaxTimeParce = x.ParcedLinks.Max(x => x.Time),
                         MinTimeParce = x.ParcedLinks.Min(x => x.Time),
                         Id = x.Id,
                         DateOfParce = x.DateOfParce,
                         Link = x.Name,
                     })
                     .OrderByDescending(x => x.DateOfParce)
                     .AsQueryable();
            return links;
        }

        public IQueryable<ParcedLink> GetParcedLinksById(int id)
        {
            var queryable = _context.ParcedLinks.Select(x => x).Where(x => x.LinkId == id).OrderBy(x => x.Time).AsQueryable();

            if (!queryable.Any())
                return null;

            return queryable;
        }

        public async Task<List<LinkTime>> ParceAsync(string url)
        {
            var parceHashSet = await ParcerXmlFile.Parce(url);
            var parceList = CreaterListOfLinks.Create(parceHashSet);
            SorterLinkTime.Sort(ref parceList);
            await CreateAsync(url, parceList);
            return parceList;
        }
    }
}
