using APITrip.models.Agences;
using APITrip.Services;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencesController : ControllerBase
    {
        private readonly IAgenceService _agenceService;
        public AgencesController(IAgenceService agenceService)
        {
            _agenceService = agenceService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var agences = _agenceService.GetAll();
            return Ok(agences);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var agence = _agenceService.GetById(id);
            if (agence == null)
                return NotFound(new { message = "Agence not found" });
            return Ok(agence);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AgenceCreateRequest model)
        {
            _agenceService.Create(model);
            return Created("", new { message = "Agence created" });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AgenceUpdateRequest model)
        {
            _agenceService.Update(id, model);
            return Ok(new { message = "Agence updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _agenceService.Delete(id);
            return NoContent();
        }
    }
}
