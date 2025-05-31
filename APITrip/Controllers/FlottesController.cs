using APITrip.models.Flotte;
using APITrip.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlottesController : ControllerBase
    {
        private IFlotteService _flotteService;
        private IMapper _mapper;

        public FlottesController(
            IFlotteService flotteService,
            IMapper mapper)
        {
            _flotteService = flotteService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var flottes = _flotteService.GetAll();
                return Ok(flottes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des flottes : " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var flotte = _flotteService.GetById(id);
                if (flotte == null)
                    return NotFound(new { message = "Flotte non trouvée" });
                return Ok(flotte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération de la flotte : " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] FlotteCreateRequest model)
        {
            try
            {
                _flotteService.Create(model);
                return StatusCode(201, new { message = "Flotte créée avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la création : " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FlotteUpdateRequest model)
        {
            try
            {
                _flotteService.Update(id, model);
                return Ok(new { message = "Flotte modifiée avec succès" });
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
                _flotteService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la suppression : " + ex.Message });
            }
        }
    }
}
