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
        public IActionResult GetAll() => Ok(_service.GetAll());
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_service.GetById(id));
        [HttpPost]
        public IActionResult Create(CreateRequest model) { _service.Create(model); return Ok(new { message = "Gestionnaire created" }); }
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model) { _service.Update(id, model); return Ok(new { message = "Gestionnaire updated" }); }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _service.Delete(id); return Ok(new { message = "Gestionnaire deleted" }); }
    }
}
