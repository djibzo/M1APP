using APITrip.Entities;
using APITrip.models.Flotte;
using APITrip.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlottesController : ControllerBase
    {
        private IFlotteService _flotteService;
        private IMapper _mapper;

        public FlottesController(
            IFlotteService flotteService,
            IMapper mapper)
        {
            _flotteService = flotteService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var flottes = _flotteService.GetAll();
            return Ok(flottes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var flotte = _flotteService.GetById(id);
            return Ok(flotte);
        }

        [HttpPost]
        public IActionResult Create([FromBody] FlotteCreateRequest model)
        {
            _flotteService.Create(model);
            return Created("", new { message = "Flotte created"});
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FlotteUpdateRequest model)
        {
            _flotteService.Update(id, model);
            return Ok(new { message = "Flotte updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _flotteService.Delete(id);
            return NoContent();
        }
    }
}
