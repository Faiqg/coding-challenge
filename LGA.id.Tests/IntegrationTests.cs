using Data.ViewModel;
using Logic.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LGA.id.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private readonly LGAContext _context = new LGAContext();
        
        [TestMethod]
        public void GetStateTest()
        {
            IStateService stateService = new StateService(_context);
            var state = stateService.GetState(3);
            Assert.IsTrue(String.Equals("Queensland", state.StateName, StringComparison.CurrentCultureIgnoreCase));
        }
        [TestMethod]
        public void ReportServiceIntegrationTest()
        {
            IReportService reportService = new ReportService(_context);
            //for QLD expect 36 results
            var searchParams = new ReportSearchModel()
            {
                FirstYear = 2011,
                SecondYear = 2016,
                StateId = 3
            };
            
            var result = reportService.GetDisadges(searchParams);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ReportModel>));
            Assert.AreEqual(36, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(item.DisadvantageFirstYear > 949, "Disavantage for first year is smaller than or equal to state median");
                Assert.IsTrue(item.DisadvantageSecondYear > 949, "Disavantage for second year is smaller than or equal to state median");
            }
        }
    }
}
