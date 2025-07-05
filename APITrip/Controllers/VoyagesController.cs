using APITrip.models.Voyages;
using APITrip.Services;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoyagesController : ControllerBase
    {
        private readonly VoyageService _service = new();
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_service.GetById(id));
        [HttpPost]
        public IActionResult Create([FromBody] VoyageCreateRequest model) { _service.Create(model); return Created("", new { message = "Voyage created" }); }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] VoyageUpdateRequest model) { _service.Update(id, model); return Ok(new { message = "Voyage updated" }); }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _service.Delete(id); return NoContent(); }
    }
}
