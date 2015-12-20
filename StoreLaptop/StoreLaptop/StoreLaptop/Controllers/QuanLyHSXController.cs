using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
namespace StoreLaptop.Controllers
{
    public class QuanLyHSXController : Controller
    {
        // GET: QuanLyHSX
        Service1Client db = new Service1Client();
        public ActionResult Index()
        {
            ViewBag.hsx = db.LayHangsx().OrderBy(n=>n.TenHSX);
            return View();
        }
        [HttpGet]
        public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Them(HangSanXuat HSX ,FormCollection f)
        {
            HSX.TenHSX = f["TenHSX"];
            HSX.AnHien = int.Parse(f["AnHien"]);
            db.ThemHSX(HSX);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Sua(int id)
        {
            ViewBag.hsx = db.LayHangsx().Where(n => n.idHSX == id);
            return View();
        }
        [HttpPost]
        public ActionResult Sua(HangSanXuat HSX, FormCollection f,int id)
        {
            HSX.idHSX = id;
            HSX.TenHSX = f["TenHSX"];
            HSX.AnHien = int.Parse(f["AnHien"]);
            db.SuaHSX(HSX);
           
            if (HSX.AnHien == 0)
            {
                var sp = db.LaySPHsx(id);
                foreach (var i in sp)
                {
                    i.AnHien = 0;
                    db.ChinhSua(i);
                }
                return RedirectToAction("Index");
            } 
            return RedirectToAction("Index");
        }
    }
}