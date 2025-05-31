using APITrip.models.Chauffeurs;
using APITrip.Services;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChauffeursController : ControllerBase
    {
        private readonly ChauffeurService _service = new();
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_service.GetById(id));
        [HttpPost]
        public IActionResult Create([FromBody] ChauffeurCreateRequest model) { _service.Create(model); return Ok(new { message = "Chauffeur created" }); }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ChauffeurUpdateRequest model) { _service.Update(id, model); return Ok(new { message = "Chauffeur updated" }); }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _service.Delete(id); return Ok(new { message = "Chauffeur deleted" }); }
    }
}
