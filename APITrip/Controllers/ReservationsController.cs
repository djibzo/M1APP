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
        public IActionResult GetAll() => Ok(_service.GetAll());
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_service.GetById(id));
        [HttpPost]
        public IActionResult Create([FromBody] ReservationCreateRequest model) { _service.Create(model); return Ok(new { message = "Reservation created" }); }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReservationUpdateRequest model) { _service.Update(id, model); return Ok(new { message = "Reservation updated" }); }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _service.Delete(id); return Ok(new { message = "Reservation deleted" }); }
    }
}
