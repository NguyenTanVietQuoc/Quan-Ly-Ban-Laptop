using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;

namespace StoreLaptop.Controllers
{
    public class QuanLyKHController : Controller
    {
        // GET: QuanLyKH
        Service1Client db = new Service1Client();
        public ActionResult Index()
        {
            ViewBag.kh = db.LayKH();
            return View();
        }
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Duyet()
        {
            if (Session["Admin"] == null) { return RedirectToAction("Index", "Admin"); }
            else
            {
                return RedirectToAction("Index","QuanLyKH");
            }

        }
        [HttpPost]
        public ActionResult Duyet(int idKH, KhachHang khachhang)
        {
            if (Session["Admin"] == null) { return RedirectToAction("Index", "Admin"); }
            else
            {
                khachhang.idKH = idKH;
                db.Duyet(khachhang);
                return RedirectToAction("Index", "QuanLyKH");
            }
        }

        public ActionResult XoaKH(KhachHang kh,int id)
        {
            kh.idKH = id;
            db.XoaKH(kh);
            return RedirectToAction("Index", "QuanLyKH");
        }
    }
}