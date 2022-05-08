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
        public ParcerXmlRepository(ParcerContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(string url, List<LinkTime> linkTimes)
        {
            /*var link = await _context.Links.FirstOrDefaultAsync(x => x.Name == url);
            if (link != null)
            {
                _context.Links.Remove(link);
                await _context.SaveChangesAsync();
            }

            var parcedLinks = linkTimes.Select(x => new ParcedLink { Name = x.Link, Time = x.Time, Link = link }).ToList();
            link = new Link { Name = url, ParcedLinks = parcedLinks };
            _context.Links.Add(link);*/
            var link = new Link { Name = url };
            var parcedLinks = linkTimes.Select(x => new ParcedLink { Name = x.Link, Time = x.Time, Link = link }).ToList();

            await _context.ParcedLinks.AddRangeAsync(parcedLinks);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<LinkModel> GetAsync()
        {
            var links = _context.Links.Select(x => new LinkModel { Link = x.Name, Id = x.Id }).ToList();
            return links;
        }

        public IQueryable<ParcedLink> GetById(PaginationDTO model)
        {
            //var allLinksById = await _context.Links.Include(x => x.ParcedLinks).Where(x=> x.Id == model.Id).FirstOrDefaultAsync();
            //num 1
            //size 15
            //take 15
            /*int skipedLinks = (model.Page - 1) * model.PageSize;*/
            var queryable = _context.ParcedLinks.Select(x => x).Where(x => x.LinkId == model.Id).AsQueryable();


            /* var parcedLinksByPage = await _context.ParcedLinks
                 .Select(x => x)
                 .Where(x => x.LinkId == model.Id)
                 .Skip(skipedLinks)
                 .Take(model.PageSize)
                 .ToListAsync();*/

            /*if (parcedLinksByPage.Count == 0)
                return null; */
            if (queryable.Count() == 0)
                return null;

            return queryable;

            /*if (allLinksById == null*//* || skipedLinks >= allLinksById.ParcedLinks.Count*//*)
                return null;*/


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
