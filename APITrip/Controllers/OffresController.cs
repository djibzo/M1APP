using APITrip.models.Offres;
using APITrip.Services;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffresController : ControllerBase
    {
        private readonly OffreService _service = new();
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_service.GetById(id));
        [HttpPost]
        public IActionResult Create([FromBody] OffreCreateRequest model) { _service.Create(model); return Ok(new { message = "Offre created" }); }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OffreUpdateRequest model) { _service.Update(id, model); return Ok(new { message = "Offre updated" }); }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _service.Delete(id); return Ok(new { message = "Offre deleted" }); }
    }
}
