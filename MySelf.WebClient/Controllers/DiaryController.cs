using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySelf.WebClient.Models;

namespace MySelf.WebClient.Controllers
{
    public class DiaryController : Controller
    {
        public ActionResult Index(string link)
        {
            var model = new DiaryViewModel {Link = link};
            return View(model);
        }
    }
}
