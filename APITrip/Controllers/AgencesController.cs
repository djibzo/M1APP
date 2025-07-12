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
        private readonly APITrip.Kafka.KafkaProducerService _kafkaProducerService;
        private readonly APITrip.Redis.RedisService _redisService;

        public AgencesController(IAgenceService agenceService, APITrip.Kafka.KafkaProducerService kafkaProducerService, APITrip.Redis.RedisService redisService)
        {
            _agenceService = agenceService;
            _kafkaProducerService = kafkaProducerService;
            _redisService = redisService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var agences = _agenceService.GetAll();
            return Ok(agences);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var agence = _agenceService.GetById(id);
            if (agence == null)
                return NotFound(new { message = "Agence not found" });
            return Ok(agence);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AgenceCreateRequest model)
        {
            _agenceService.Create(model);
            // Envoi d'un message Kafka après la création
            await _kafkaProducerService.ProduceAsync($"Agence créée: {System.Text.Json.JsonSerializer.Serialize(model)}");
            // Stockage dans Redis (clé = agence:{nom}, valeur = JSON)
            await _redisService.SetAgenceAsync($"agence:{model.AdresseAgence}", System.Text.Json.JsonSerializer.Serialize(model));
            return Created("", new { message = "Agence created, Kafka message sent, and saved in Redis" });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AgenceUpdateRequest model)
        {
            _agenceService.Update(id, model);
            return Ok(new { message = "Agence updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _agenceService.Delete(id);
            return NoContent();
        }
    }
}
