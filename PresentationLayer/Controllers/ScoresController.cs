using Logic.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class ScoresController : Controller
    {
        private readonly IScoreService _scoreService;
        private readonly IStateService _stateService;
        public ScoresController(IScoreService scoreService, IStateService stateService)
        {
            _scoreService = scoreService;
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
            return View(_scoreService.GetAllData(null));
        }
        public ActionResult DataGrid(string stateId)
        {
            int id = stateId != null ? int.Parse(stateId) : -1;
            return PartialView(_scoreService.GetAllData(id));
        }
    }
}