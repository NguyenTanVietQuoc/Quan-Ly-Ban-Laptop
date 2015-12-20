using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;

namespace StoreLaptop.Controllers
{
    public class HomeController : Controller
    {
        Service1Client db = new Service1Client();
        public ActionResult Index()
        {
            ViewBag.index = db.LaySPindex().Where(x=>x.AnHien ==1);
            return View();
        }

       
    }
}