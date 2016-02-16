using System.Web.Mvc;

namespace MySelf.WebClient.Controllers
{
    public class DiaryController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
