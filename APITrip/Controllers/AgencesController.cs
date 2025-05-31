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
            try
            {
                var agences = _agenceService.GetAll();
                return Ok(agences);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des agences : " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var agence = _agenceService.GetById(id);
                if (agence == null)
                    return NotFound(new { message = "Agence non trouvée" });
                return Ok(agence);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération de l'agence : " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] AgenceCreateRequest model)
        {
            try
            {
                _agenceService.Create(model);
                return StatusCode(201, new { message = "Agence créée avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la création : " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AgenceUpdateRequest model)
        {
            try
            {
                _agenceService.Update(id, model);
                return Ok(new { message = "Agence modifiée avec succès" });
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
                _agenceService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la suppression : " + ex.Message });
            }
        }
    }
}
