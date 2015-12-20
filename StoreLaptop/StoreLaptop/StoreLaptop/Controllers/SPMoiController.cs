using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
namespace StoreLaptop.Controllers
{
    public class SPMoiController : Controller
    {
        // GET: SPMoi
        Service1Client db = new Service1Client();
        public PartialViewResult SPMoiPartial()
        {
            ViewBag.SPmoi = db.LaySPmoi();
            return PartialView();
        }
    }
}