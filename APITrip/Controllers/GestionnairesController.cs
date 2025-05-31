using APITrip.models.Gestionnaires;
using APITrip.Services;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionnairesController : ControllerBase
    {
        private readonly GestionnaireService _service = new();
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var gestionnaires = _service.GetAll();
                return Ok(gestionnaires);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des gestionnaires : " + ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var gestionnaire = _service.GetById(id);
                if (gestionnaire == null)
                    return NotFound(new { message = "Gestionnaire non trouvé" });
                return Ok(gestionnaire);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération du gestionnaire : " + ex.Message });
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] GestionnaireCreateRequest model)
        {
            try
            {
                _service.Create(model);
                return StatusCode(201, new { message = "Gestionnaire créé avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la création : " + ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GestionnaireUpdateRequest model)
        {
            try
            {
                _service.Update(id, model);
                return Ok(new { message = "Gestionnaire modifié avec succès" });
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
