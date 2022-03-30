using System.ComponentModel.DataAnnotations;

namespace SatelliteOperatorExercise.Model
{
    public class CollisionEvent
    {
        public string? MessageID { get; set; }
        public string? EventID { get; set; }
        public string? SatelliteID { get; set; }
        public string? OperatorID { get; set; }
        public float ProbabilityOfCollision { get; set; }
        public DateTime CollisionDate { get; set; }
        public string? ChaserObjectID { get; set; }
    }

    public class CollisionEventDTO : IValidatableObject
    {
        public string? MessageID { get; set; }
        public string? EventID { get; set; }
        public string? SatelliteID { get; set; }
        public string? OperatorID { get; set; }
        public float ProbabilityOfCollision { get; set; }
        public DateTime CollisionDate { get; set; }
        public string? ChaserObjectID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new();
            
            if (ProbabilityOfCollision < 0 || ProbabilityOfCollision > 1)
                result.Add(new ValidationResult($"Probability of collision should be between 0 and 1."));

            TimeSpan diff = DateTime.Now - CollisionDate;

            float year = diff.Days / 365;

            if (year > 1)
                result.Add(new ValidationResult($"The Collision date should not be over 1 year old."));

            return result;
        }
    }
}