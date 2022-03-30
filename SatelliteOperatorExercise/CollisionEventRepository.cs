using AutoMapper;
using Bogus;
using SatelliteOperatorExercise.Model;

namespace SatelliteOperatorExercise.API
{
    public class CollisionEventRepository : ICollisionEventRepository
    {
        static List<CollisionEvent> collisionsList;

        // define the mapper
        readonly IMapper _mapper;

        public CollisionEventRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// I used static constructor to call the constructor only once to fill the collisionList variable
        /// </summary>
        static CollisionEventRepository()
        {
            //Setup for generating fake data
            var collisionFaker = new Faker<CollisionEvent>()
                .RuleFor(r => r.MessageID, f => f.Random.AlphaNumeric(10))
                .RuleFor(r => r.EventID, f => f.Random.AlphaNumeric(10))
                .RuleFor(r => r.SatelliteID, f => f.Random.AlphaNumeric(10))
                .RuleFor(r => r.OperatorID, f => f.Random.AlphaNumeric(10))
                .RuleFor(r => r.ProbabilityOfCollision, f => f.Random.Float())
                .RuleFor(r => r.CollisionDate, f => f.Date.Between(DateTime.Now.AddMonths(2), DateTime.Now.AddMonths(-2)))
                .RuleFor(r => r.ChaserObjectID, f => f.Random.AlphaNumeric(10));

            collisionsList = collisionFaker.Generate(250);
        }

        public async Task<List<CollisionEventDTO>> GetAllCollisionEventsAsync()
        {
            return await Task.FromResult(collisionsList.Where(c => c.ProbabilityOfCollision >= 0.75 && c.CollisionDate >= DateTime.Now)
                .Select(c => _mapper.Map<CollisionEvent, CollisionEventDTO>(c)).ToList());
        }

        public bool InsertNewCollisionEvent(CollisionEventDTO entity)
        {
            //Add the event to the list
            var mappedCollision = _mapper.Map<CollisionEvent>(entity);
            collisionsList.Add(mappedCollision);
            return true;
        }

        public async Task<CollisionEventDTO?> GetCollisionEvent(string messageID)
        {
            var collision = collisionsList.Find(m => m.MessageID == messageID);
            if (collision != null)
                return await Task.FromResult(_mapper.Map<CollisionEvent, CollisionEventDTO>(collision));
            else
                return null;
        }

        public async Task<CollisionEventDTO?> DeleteCollisionEvent(string messageID)
        {
            var collision = collisionsList.Find(m => m.MessageID == messageID);
            if (collision != null)
            {
                await Task.FromResult(collisionsList.Remove(collision));
                return _mapper.Map<CollisionEvent, CollisionEventDTO>(collision);
            }
            else
                return null;
        }
    }
}