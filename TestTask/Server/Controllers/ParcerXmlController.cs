using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParcerLibrarry;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Server.Repositories;
using TestTask.Shared;

namespace TestTask.Server.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ParcerXmlController : ControllerBase
    {
        private readonly IParcerXmlRepository _parcerXmlRepository;
        public ParcerXmlController(IParcerXmlRepository parcerXmlRepository)
        {
            _parcerXmlRepository = parcerXmlRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var links = _parcerXmlRepository.Get();
            if(links == null)
                return NoContent();

            return Ok(links);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var link = await _parcerXmlRepository.GetById(id);
            if(link == null)
                return NotFound();

            return Ok(link);
        }


        [HttpPost]
        public async Task<IActionResult> Parce(LinkModel model)
        {
            if(model == null || String.IsNullOrEmpty(model.Link)) 
            {
                ModelState.AddModelError("link", "link is empty");
                return BadRequest(ModelState); 
            }

            try
            {
               var parceList = await _parcerXmlRepository.Parce(model.Link);
               return Ok(parceList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("link", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
