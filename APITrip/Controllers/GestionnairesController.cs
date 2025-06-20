using APITrip.models.Gestionnaires;
using APITrip.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionnairesController : ControllerBase
    {
        private IGestionnaireService _service;
        private IMapper _mapper;
        public GestionnairesController(
        IGestionnaireService service,
        IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var gestionnaires = _service.GetAll();
            return Ok(gestionnaires);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var gestionnaires = _service.GetById(id);
            return Ok(gestionnaires);
        }
        [HttpPost]
        public IActionResult Create(GestionnaireCreateRequest model) {
            _service.Create(model);
            return Created("", new { message = "Gestionnaire created" });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GestionnaireUpdateRequest model)
        { _service.Update(id, model); return Ok(new { message = "Gestionnaire updated" }); }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _service.Delete(id); return NoContent(); }
    }
}
