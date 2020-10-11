using Data.ViewModel;
using Logic.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PresentationLayer.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LGA.id.Tests
{
    [TestClass]
    public class ControllerTests
    {
        private Mock<IReportService> _reportService;
        private Mock<IStateService> _stateService;
        private ReportController _reportController;
        [TestInitialize]
        public void SetUp()
        {
            _reportService = new Mock<IReportService>();
            _stateService = new Mock<IStateService>();
            _reportController = new ReportController(_reportService.Object, _stateService.Object);
            
        }
        [TestMethod]
        public void ReportControllerTest()
        {
            ViewResult result = (ViewResult)_reportController.Index();
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<ReportModel>));
        }

        [TestMethod]
        public void ReportControllerValueTest()
        {
            var expectedData = new List<ReportModel>
             {
                 new ReportModel()
                 {
                     DisadvantageFirstYear = 100,
                     DisadvantageSecondYear = 101
                 } 
            };
            _reportService
                .Setup(it => it.GetDisadges(It.IsAny<ReportSearchModel>()))
                .Returns(expectedData);
            var result = _reportController.Index();
            var model = (result as ViewResult).Model as IEnumerable<ReportModel>;
            Assert.AreEqual(model.First().Comparison, expectedData.First().Comparison);
            Assert.AreEqual(model.First().DisadvantageFirstYear, expectedData.First().DisadvantageFirstYear);
            Assert.AreEqual(model.First().DisadvantageSecondYear, expectedData.First().DisadvantageSecondYear);
        }

        [TestMethod]
        public void HomeControllerTest()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
