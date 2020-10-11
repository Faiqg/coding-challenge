using Data.ViewModel;
using Logic.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IStateService _stateService;
        public ReportController(IReportService service, IStateService stateService)
        {
            _reportService = service;
            _stateService = stateService;
        }

        public ActionResult Index()
        {
            ViewData["States"] = _stateService.GetAllStates().Select(p => new SelectListItem()
            {
                Value = p.StateId.ToString(),
                Text = p.StateName,
                Selected = (p.StateName.Equals("Victoria", StringComparison.CurrentCultureIgnoreCase))
            })
			.ToList();
            return View(_reportService.GetDisadges(new ReportSearchModel()));
        }

        public ActionResult ReportGrid(string stateId, int firstYear, int secondYear)
        {
            var searchParams = new ReportSearchModel()
            {
                StateId = stateId != null ? int.Parse(stateId) : -1,
                FirstYear = firstYear,
                SecondYear = secondYear
            };
            return PartialView(_reportService.GetDisadges(searchParams));
        }
    }
}
