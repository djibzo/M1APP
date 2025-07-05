using APITrip.models.Chauffeurs;
using APITrip.Services;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChauffeursController : ControllerBase
    {
        private readonly IChauffeurService _service;

        public ChauffeursController(IChauffeurService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var chauffeurs = _service.GetAll();
                return Ok(chauffeurs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des chauffeurs : " + ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var chauffeur = _service.GetById(id);
                if (chauffeur == null)
                    return NotFound(new { message = "Chauffeur non trouvé" });
                return Ok(chauffeur);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération du chauffeur : " + ex.Message });
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] ChauffeurCreateRequest model)
        {
            try
            {
                _service.Create(model);
                return StatusCode(201, new { message = "Chauffeur créé avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la création : " + ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ChauffeurUpdateRequest model)
        {
            try
            {
                _service.Update(id, model);
                return Ok(new { message = "Chauffeur modifié avec succès" });
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
