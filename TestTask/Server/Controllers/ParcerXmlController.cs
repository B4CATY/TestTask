using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Server.Data;
using TestTask.Server.Helpers;
using TestTask.Server.Repositories;
using TestTask.Shared;
using TestTask.Shared.Pagination;

namespace TestTask.Server.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ParcerXmlController : ControllerBase
    {
        private readonly IParcerXmlRepository _parcerXmlRepository;
        public ParcerXmlController(IParcerXmlRepository parcerXmlRepository, ParcerContext context) 
            => _parcerXmlRepository = parcerXmlRepository;

        [HttpGet]
        [Route("links")]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO model)
        {
            /*var links = _parcerXmlRepository.GetAsync();
            if(links == null)
                return NoContent();

            return Ok(links);*/
            if (model == null)
                return BadRequest();
            try
            {
                var queryable = _parcerXmlRepository.GetLinks();

                if (queryable == null)
                    return NotFound();

                await HttpContext.InsertPaginationParametrInResponse(queryable, model.PageSize);
                var resultLinks = await queryable.Paginate(model).ToListAsync();
               
                return Ok(resultLinks);

            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] PaginationDTO model)
        {
            if (model == null)
                return BadRequest();
            try
            {
                var queryable = _parcerXmlRepository.GetParcedLinksById(model.Id);

                if (queryable == null)
                    return NotFound();

                await HttpContext.InsertPaginationParametrInResponse(queryable, model.PageSize);
                var resultLinks = await queryable.Paginate(model).ToListAsync();

                return Ok(resultLinks);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Parce(LinkModel model)
        {
            if (model == null || String.IsNullOrEmpty(model.Link))
                return BadRequest();

            try
            {
                var parceList = await _parcerXmlRepository.ParceAsync(model.Link);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
