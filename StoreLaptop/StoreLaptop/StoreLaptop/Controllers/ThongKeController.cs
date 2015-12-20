using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;

namespace StoreLaptop.Controllers
{
    public class ThongKeController : Controller
    {
        Service1Client db = new Service1Client();
        // GET: ThongKe
        public ActionResult Index()
        {
           int?[]a =new int?[10];
            decimal?[]gia = new decimal?[10];
            var hsx = db.LayHangsx();
            ViewBag.hsx = hsx;
            foreach (var i in hsx)
            {
                int? s=0;
                decimal? t = 0;
                var sp = db.LaySPHsx(i.idHSX);
                foreach (var j in sp)
                {                    
                    s+=j.SoLanMua;
                    t+=(j.SoLanMua*j.Gia);
                    
                }
                a[i.idHSX] = s;
                gia[i.idHSX] = t;
            }
            ViewBag.a = a;
            ViewBag.gia = gia;
            return View();
        }
    }
}