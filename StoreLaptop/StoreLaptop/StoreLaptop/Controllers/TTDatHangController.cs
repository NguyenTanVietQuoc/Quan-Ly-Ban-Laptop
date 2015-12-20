using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
namespace StoreLaptop.Controllers
{
    public class TTDatHangController : Controller
    {
        // GET: TTDatHang
        Service1Client db = new Service1Client();
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        public ActionResult TTDatHang(string thongbao)
        {
            ViewBag.thongbao = thongbao;
            return View();
        }
        //[HttpPost]
        //public ActionResult TTDatHang()
        //{
        //    return View();
        //}
    }
}