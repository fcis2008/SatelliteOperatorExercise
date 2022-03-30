using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SatelliteOperatorExercise.Controllers;
using SatelliteOperatorExercise.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatelliteOperatorExercise.API.UnitTest
{
    [TestClass]
    public class CollisionEventTest
    {
        [TestInitialize]
        public void InitializeForTests()
        {

        }

        [TestMethod]
        public async Task Get_Test_Collision_Events()
        {
            //Arrange
            var collisionEventRepository = new Mock<ICollisionEventRepository>();
            var collisionEventController = new CollisionEventController(collisionEventRepository.Object);

            collisionEventRepository.Setup(repo => repo.GetAllCollisionEventsAsync()).Returns(Task.FromResult(GetTestCollisionEvents()));

            //Act
            var collisionsList = await collisionEventController.Get();

            //Assert
            Assert.IsNotNull(collisionsList);
            Assert.AreEqual(collisionsList.Count, 250);
        }

        private List<CollisionEventDTO> GetTestCollisionEvents()
        {
            //Setup for generating fake data
            var collisionFaker = new Faker<CollisionEventDTO>()
                .RuleFor(r => r.MessageID, f => f.Random.AlphaNumeric(10))
                .RuleFor(r => r.EventID, f => f.Random.AlphaNumeric(10))
                .RuleFor(r => r.SatelliteID, f => f.Random.AlphaNumeric(10))
                .RuleFor(r => r.OperatorID, f => f.Random.AlphaNumeric(10))
                .RuleFor(r => r.ProbabilityOfCollision, f => f.Random.Float())
                .RuleFor(r => r.CollisionDate, f => f.Date.Between(DateTime.Now.AddMonths(2), DateTime.Now.AddMonths(-2)))
                .RuleFor(r => r.ChaserObjectID, f => f.Random.AlphaNumeric(10));

            return collisionFaker.Generate(250);
        }
    }
}