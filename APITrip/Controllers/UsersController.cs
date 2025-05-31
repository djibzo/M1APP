using APITrip.models.users;
using APITrip.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        public UsersController(
        IUserService userService,
        IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des utilisateurs : " + ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _userService.GetById(id);
                if (user == null)
                    return NotFound(new { message = "Utilisateur non trouvé" });
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération de l'utilisateur : " + ex.Message });
            }
        }
        [HttpPost]
        public IActionResult Create(CreateRequest model)
        {
            try
            {
                _userService.Create(model);
                return StatusCode(201, new { message = "Utilisateur créé avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la création : " + ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            try
            {
                _userService.Update(id, model);
                return Ok(new { message = "Utilisateur modifié avec succès" });
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
                _userService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la suppression : " + ex.Message });
            }
        }
    }
}
