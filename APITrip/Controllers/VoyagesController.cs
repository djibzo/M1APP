using APITrip.models.Voyages;
using APITrip.Services;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoyagesController : ControllerBase
    {
        private readonly IVoyageService _service;

        public VoyagesController(IVoyageService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var voyages = _service.GetAll();
                return Ok(voyages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des voyages : " + ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var voyage = _service.GetById(id);
                if (voyage == null)
                    return NotFound(new { message = "Voyage non trouvé" });
                return Ok(voyage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération du voyage : " + ex.Message });
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] VoyageCreateRequest model)
        {
            try
            {
                _service.Create(model);
                return StatusCode(201, new { message = "Voyage créé avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la création : " + ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] VoyageUpdateRequest model)
        {
            try
            {
                _service.Update(id, model);
                return Ok(new { message = "Voyage modifié avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la modification : " + ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la suppression : " + ex.Message });
            }
        }
    }
}
