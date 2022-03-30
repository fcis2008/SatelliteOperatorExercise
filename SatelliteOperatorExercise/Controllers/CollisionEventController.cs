using Microsoft.AspNetCore.Mvc;
using SatelliteOperatorExercise.API;
using SatelliteOperatorExercise.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SatelliteOperatorExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollisionEventController : ControllerBase
    {
        readonly ICollisionEventRepository repository;

        public CollisionEventController(ICollisionEventRepository _repository)
        {
            repository = _repository;
        }

        // GET: api/<CollisionController>
        [HttpGet]
        public async Task<List<CollisionEventDTO>> Get()
        {
            List<CollisionEventDTO> collisionDTOs;

            try
            {
                collisionDTOs = await repository.GetAllCollisionEventsAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return collisionDTOs;
        }

        // POST api/<CollisionController>
        [HttpPost]
        public async Task<ActionResult<CollisionEventDTO>> Post(CollisionEventDTO collisionDTO)
        {
            if (collisionDTO == null || !ModelState.IsValid)
                return BadRequest();

            //Get header value to validate the request
            if (!Request.Headers.TryGetValue("X-OperatorID", out var headerValue))
                return BadRequest("operator_id wasn't found.");

            if (headerValue != collisionDTO.OperatorID)
                return BadRequest("Collision event doesn't belong to the operator id.");

            var collision = await repository.GetCollisionEvent(collisionDTO.MessageID);

            //Check if the event already inserted
            if (collision != null && collision.MessageID == collisionDTO.MessageID)
                return Conflict(new { message = $"An existing record with the id '{collisionDTO.MessageID}' was already found." });

            bool result = repository.InsertNewCollisionEvent(collisionDTO);

            if (result)
                return Ok("Collision event has been created successfully.");
            else
                return BadRequest();
        }

        // DELETE api/<CollisionController>/5
        [HttpDelete("{messageID}")]
        public async Task<ActionResult<CollisionEventDTO>> Delete(string messageID)
        {
            var collision = await repository.GetCollisionEvent(messageID);

            if (collision == null)
                return NotFound();

            //Busniess rule validation
            if (collision.CollisionDate < DateTime.Now)
                return BadRequest(new { message = $"Collision date is not in the future." });

            if (!Request.Headers.TryGetValue("X-OperatorID", out var headerValue))
                return Conflict("operator_id wasn't found.");

            if (headerValue != collision.OperatorID)
                return BadRequest("Collision event doesn't belong to the operator id.");

            return await repository.DeleteCollisionEvent(messageID);
        }
    }
}
