using SatelliteOperatorExercise.Model;

namespace SatelliteOperatorExercise.API
{
    public interface ICollisionEventRepository
    {
        Task<List<CollisionEventDTO>> GetAllCollisionEventsAsync();
        bool InsertNewCollisionEvent(CollisionEventDTO entity);
        Task<CollisionEventDTO?> GetCollisionEvent(string messageID);
        Task<CollisionEventDTO?> DeleteCollisionEvent(string messageID);
    }
}
