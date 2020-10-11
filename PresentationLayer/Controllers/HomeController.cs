using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "iD submission";
            return View();
        }

        public ActionResult Error(string message)
        {
            ViewData["message"] = message;
            return View();
        }
    }
}