using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySelf.WebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index2");
        }

        //public ActionResult Diary(string link)
        //{
        //    return View("Diary", link);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
