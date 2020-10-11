using Data.Entites;
using Logic.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LGA.id.Tests
{
    [TestClass]
    public class LGAContextTests
    {
        [TestMethod]
        public void ReportServiceTest()
        {
            var data = new List<Score>
            {
                new Score { 
                    AdvantageDisadvantageScore = 100, 
                    DisadvantageScore = 101,
                    ScoreId = 1,
                    Year = 2000,
                    Location = new Location()
                    { 
                        LocationId = 1,
                        PlaceName = "Mockbourne",
                        State = new State() 
                        {
                            StateName = "Mocktoria", 
                            StateId = 1
                        } 
                    } 
                }                
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Score>>();
            mockSet.As<IQueryable<Score>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Score>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Score>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Score>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<LGAContext>();
            mockContext.Setup(c => c.Scores).Returns(mockSet.Object);
            var service = new ScoreService(mockContext.Object);
            var scores = service.GetScores(1, 2000);
            Assert.IsTrue(scores.Contains(scores.First(x => x.AdvantageDisadvantageScore == 100)));
            Assert.IsTrue(scores.Contains(scores.First(x => x.DisadvantageScore == 101)));
            Assert.AreEqual(scores.First(x => x.Location.LocationId == 1).Location.PlaceName, "Mockbourne");
        }
        [TestMethod]
        public void CreateStateTest()
        {
            var mockSet = new Mock<DbSet<State>>();
            var mockContext = new Mock<LGAContext>();
            mockContext.Setup(m => m.States).Returns(mockSet.Object);
            var service = new StateService(mockContext.Object);
            service.AddState(new State() { StateId = 20, StateName = "Central Australia", Median= 100});
            mockSet.Verify(m => m.Add(It.IsAny<State>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        [TestMethod]
        public void GetAllStatesTest()
        {
            var data = new List<State>
            {
                new State { StateName = "Victoria" },
                new State { StateName = "South Australia" },
                new State { StateName = "Northern Territory" },
            }.AsQueryable();
            var mockSet = new Mock<DbSet<State>>();
            mockSet.As<IQueryable<State>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<State>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<State>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<State>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<LGAContext>();
            mockContext.Setup(c => c.States).Returns(mockSet.Object);
            var service = new StateService(mockContext.Object);
            var states = service.GetAllStates();

            Assert.AreEqual(3, states.Count());
            Assert.IsTrue(states.Contains(states.First(x => x.StateName == "Victoria")));
            Assert.IsTrue(states.Contains(states.First(x => x.StateName == "Northern Territory")));
            Assert.IsTrue(states.Contains(states.First(x => x.StateName == "South Australia")));
        }
    }
}
