using Moq;
using SatelliteOperatorExercise.API;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var employeeRepository = new Mock<ICollisionEventRepository>();
        }
    }
}