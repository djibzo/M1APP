using APITrip.models.Reservations;
using APITrip.Services;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _service = new();
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var reservations = _service.GetAll();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des réservations : " + ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var reservation = _service.GetById(id);
                if (reservation == null)
                    return NotFound(new { message = "Réservation non trouvée" });
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération de la réservation : " + ex.Message });
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] ReservationCreateRequest model)
        {
            try
            {
                _service.Create(model);
                return StatusCode(201, new { message = "Réservation créée avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la création : " + ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReservationUpdateRequest model)
        {
            try
            {
                _service.Update(id, model);
                return Ok(new { message = "Réservation modifiée avec succès" });
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
